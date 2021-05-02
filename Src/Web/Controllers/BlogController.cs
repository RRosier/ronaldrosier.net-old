using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rosier.Blog.Service.ViewModel;
using Rosier.Blog.Services;
//using Rosier.Akismet.Net;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Rosier.Blog.Web.Controllers
{
    /// <summary>
    /// Controller for the blog features
    /// </summary>
    public class BlogController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        /// <param name="blogService">The service.</param>
        public BlogController(IBlogService blogService)
            : base(blogService)
        {
        }

        //
        // GET: /Blog/

        /// <summary>
        /// Lists all entries.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns></returns>
        public IActionResult Index(int page = 1)
        {
            var entries = this.blogService.GetAllEntries(page);//.GetLatestEntries();

            CommonData();

            ViewBag.Title = String.Format("Ronald Rosier .NET - (ASP).NET-C#-System Architecture-and more...", page);

            if (entries.Count() == 0)
            {
                return View("_Empty");
            }

            return View("Listing", entries);
        }

        /// <summary>
        /// Displays the specified entry.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public IActionResult Entry(int year, int month, int day, string title)
        {
            CommonData();

            var entry = this.blogService.GetEntry(year, month, day, title);

            if (entry == null)
                return View("NotFound");

            ViewBag.Title = entry.Title;

            return View("Entry", entry);
        }

        /// <summary>
        /// Lists the entries by day.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="page">The page number.</param>
        /// <returns></returns>
        public IActionResult ListByDay(int year, int month, int day, int page = 1)
        {
            CommonData();

            var entries = this.blogService.GetEntriesByDay(year, month, day, page);

            ViewBag.Title = String.Format("Ronald Rosier.NET - Entry Listing for '{0}/{1}/{2}' (page {3})", day, month, year, page);

            if (entries.Count() == 0)
                return View("NotFound");


            return View("Listing", entries);
        }

        /// <summary>
        /// Lists the entries by month.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="page">The page number.</param>
        /// <returns></returns>
        public IActionResult ListByMonth(int year, int month, int page = 1)
        {
            CommonData();
            var entries = this.blogService.GetEntriesByMonth(year, month, page);

            ViewBag.Title = String.Format("Ronald Rosier.NET - Entry Listing for '{0}/{1}' (page {2})", month, year, page);

            if (entries.Count() == 0)
                return View("NotFound");

            return View("Listing", entries);
        }

        /// <summary>
        /// Lists the entries by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="page">The page number.</param>
        /// <returns></returns>
        public IActionResult ListByYear(int year, int page = 1)
        {
            CommonData();
            var entries = this.blogService.GetEntriesByYear(year, page);

            ViewBag.Title = String.Format("Ronald Rosier.NET - Entry Listing for '{0}' (page {1})", year, page);

            if (entries.Count() == 0)
                return View("NotFound");

            return View("Listing", entries);
        }

        [HttpPost]
        [Authorize] // TODO-rro: temporarly disable this action comment until further notice
        public async Task<IActionResult> AddComment(CommentEditViewModel comment)
        {
            return Json(new { Success = false, Messeage = "Due to immens spam on this blog, comments are closed for everyone until I find a way to fight them." });

            if (!ModelState.IsValid)
                return Json(new { Success = false });

            // TODO-rro: use IoC to resolve service.
            //var ipaddress = ControllerContext.HttpContext.Request.ServerVariables["REMOTE_HOST"];
            //var userAgent = ControllerContext.HttpContext.Request.UserAgent;
            string ipaddress, userAgent;

            // TODO-rro: Place this in a decent configuration mechanisme
            var akismetKey = ConfigurationManager.AppSettings["AkismetKey"];
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            var appVersion = ConfigurationManager.AppSettings["AppVersion"];

            ISpamService spamService = new AkismetSpamService(akismetKey, baseUrl, appVersion);
            await spamService.VerifyKeyAsync();
            var isSpam = await spamService.VerifySpamAsync(comment, ipaddress, userAgent);
            if (isSpam)
                return Json(new { Success = false, Message = "Your comment has been verified as Spam by Akismet and will therefore not be posted on this site." });

            var displayComment = this.blogService.AddComment(comment);
            if (displayComment != null)
            {
                displayComment.Success = true;
                return Json(displayComment);
            }

            else
                return Json(new { Success = false });
        }

        public IActionResult CreateCaptcha()
        {
            var captchaRequest = new CaptchaRequest();
            captchaRequest.PublicKey = ConfigurationManager.AppSettings["ReCaptchaPublicKey"];
            return PartialView("_Captcha", captchaRequest);
        }

        [HttpPost]
        public IActionResult ValidateCaptcha(CaptchaRequest captcha)
        {
            //var clientIp = Request.ServerVariables["REMOTE_ADDR"];
            string clientIp = "";
            var privateKey = ConfigurationManager.AppSettings["ReCaptchaPrivateKey"];

            string data = string.Format("privatekey={0}&remoteip={1}&challenge={2}&response={3}", privateKey, clientIp, captcha.Challenge, captcha.Response);
            byte[] byteArray = new ASCIIEncoding().GetBytes(data);

            WebRequest request = WebRequest.Create("http://www.google.com/recaptcha/api/verify");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            var status = (((HttpWebResponse)response).StatusCode);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            var responseLines = responseFromServer.Split(new string[] { "\n" }, StringSplitOptions.None);
            var success = responseLines[0].Equals("true");

            return Json(new { Success = success });
        }

    }
}
