using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rosier.Blog.Services;
using Rosier.Blog.Service.ViewModel;
using System.Net.Mail;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Rosier.Blog.Web.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController(IBlogService service):base(service)
        {
            ViewBag.ActivePage = "Contact";
        }

        //
        // GET: /Contact/

        public IActionResult Index()
        {
            this.CommonData();
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel contactInfo)
        {
            this.CommonData();

            var receiver = new MailAddress(ConfigurationManager.AppSettings["ToMail"], "Ronald");
            var sender = new MailAddress(contactInfo.Email, contactInfo.Name);

            var message = new MailMessage(sender, receiver);
            message.Subject = "[Contact Blog] "+contactInfo.Subject;
            message.Body = contactInfo.Content;
            message.IsBodyHtml = false;

            var smtp = new SmtpClient();
            try
            {
                smtp.Send(message);
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false });
            }
        }
    }
}
