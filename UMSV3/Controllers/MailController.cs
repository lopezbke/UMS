using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMSV3.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            var a = new Microsoft.Office.Interop.Outlook.Application(); 
            var b = new Microsoft.Office.Interop.Outlook.MailItem();
            
            return View();
        }
    }
}