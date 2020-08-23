using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Service.ViewModel
{
    public class CaptchaRequest
    {
        public string Challenge { get; set; }
        public string Response { get; set; }
        public string PublicKey { get; set; }
    }
}
