using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMSV3.Models;
using System.Data.Entity;
namespace UMSV3.Controllers

{
    public class UserCredentialsController : Controller
    {
        private UMSEntities db = new UMSEntities();
        // GET: UserCredentials
        public ActionResult AddUserCredential(int userId)
        {
            int a = userId;
            var b = db.Set<UserCredential>();
            b.Add(new UserCredential {UserId = a, Password = "Welcome" });
            System.Diagnostics.Debug.WriteLine(a);
            db.SaveChanges();
            return RedirectToAction("Index","userinfo");
        }
    }
}