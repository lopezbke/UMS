using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UMSV3.Models;

namespace UMSV3.Controllers
{
    public class SecurityController : Controller
    {
        private UMSEntities db = new UMSEntities();
        // GET: Security
        public ActionResult Login()
        {
            UserInfo obj = new UserInfo();
            return View(obj);
        }
        // POST: Security
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName, Password")] UserInfo obj)
        {
            System.Diagnostics.Debug.WriteLine(obj.UserName);
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            System.Diagnostics.Debug.WriteLine(errors);
            if (ModelState.IsValid)
            {
                var isUserName = db.UserInfoes.Any(x => x.UserName == obj.UserName);
                var isPassword = db.UserCredentials.Any(y => y.Password == obj.Password);
               /* string query1 = "SELECT TOP 1 UserId FROM UserInfo WHERE UserName = @userName";
                string query2 = "SELECT TOP 1 Password FROM UserCredentials WHERE UserId = @userId";
                var userId = db.UserInfoes.SqlQuery(query1, new SqlParameter("@userName",obj.UserName));
                var isPassword = db.UserCredentials.SqlQuery(query2, new SqlParameter("@userId", userId));
                System.Diagnostics.Debug.WriteLine(userId);
                System.Diagnostics.Debug.WriteLine(isPassword);
*/
                if (isUserName && isPassword)
                {
                    FormsAuthentication.SetAuthCookie(obj.UserName, false);
                    Session.Add("UserName", obj.UserName);
                    FormsAuthentication.SetAuthCookie(obj.Password, false);
                    Session.Add("Password", obj.Password);
                    System.Diagnostics.Debug.WriteLine("User Matches");
                }
            }
            return RedirectToAction("Index", "userinfo");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPass() 
        {
            return View();
        }
    }
}