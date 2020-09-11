using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMSV3.Models;
namespace UMSV3.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Mail()
        {
            var a = new Microsoft.Office.Interop.Outlook.Application(); 
            var b = new Microsoft.Office.Interop.Outlook.MailItem();
            return null;
        }
    }
}