using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UMSV3.Models;

namespace UMSV3.Controllers
{
    public class UserInfoController : Controller
    {
        private UMSEntities db = new UMSEntities();

        public ActionResult Login() {
            return View();
        }
        // GET: UserInfo
        public ActionResult Index()
        {
            var userInfoes = db.UserInfoes.Include(u => u.Role).Include(u => u.Status).Include(u => u.UserCredential);
            return View(userInfoes.ToList());
        }

        // GET: UserInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfo/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName");
            ViewBag.UserId = new SelectList(db.UserCredentials, "UserId", "Password");
            return View();
        }

        // POST: UserInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,FirstName,LastName,Email,C_Address,City,Country,ZipCode,PhoneNumber,StatusId,RoleId")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfoes.Add(userInfo);
                db.SaveChanges();
                var emailEmail = userInfo.Email;
                var emailFirstName = userInfo.FirstName;
                var emailLastName = userInfo.LastName;
                var emailUserName = userInfo.UserName;


                return RedirectToAction("SendEmail", new { email = emailEmail, name = emailFirstName, lastName = emailLastName, userName = emailUserName });
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userInfo.RoleId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName", userInfo.StatusId);
            ViewBag.UserId = new SelectList(db.UserCredentials, "UserId", "Password", userInfo.UserId);
            return View(userInfo);
        }

        public ActionResult SendEmail(string email, string name, string lastName, string userName)
        {
            Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
            /*Microsoft.Office.Interop.Outlook.MailItem mailItem = new Microsoft.Office.Interop.Outlook.MailItem();*/
            Microsoft.Office.Interop.Outlook.MailItem mailItem = (Microsoft.Office.Interop.Outlook.MailItem)application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            mailItem.To = email;
            mailItem.Subject = "Setup Password";
            mailItem.HTMLBody = "Hello " + name + "," + "<br>" + "<br>" + "Your login username is: " + userName + "<br>" + "Please visit the link below to create a new password:" + "<br>" + $"<a href='https:localhost:44341/userinfo/NewPassword?name={name}'> Setup Password</a>";
            mailItem.Send();
            string a = email;
            System.Diagnostics.Debug.WriteLine(email);
            return RedirectToAction("Index");
        }

        // GET: UserInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userInfo.RoleId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName", userInfo.StatusId);
            ViewBag.UserId = new SelectList(db.UserCredentials, "UserId", "Password", userInfo.UserId);
            return View(userInfo);
        }

        // POST: UserInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,FirstName,LastName,Email,C_Address,City,Country,ZipCode,PhoneNumber,StatusId,RoleId")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userInfo.RoleId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName", userInfo.StatusId);
            ViewBag.UserId = new SelectList(db.UserCredentials, "UserId", "Password", userInfo.UserId);
            return View(userInfo);
        }

        // GET: UserInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UserInfoes.Find(id);
            db.UserInfoes.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NewPassword(string name)
            {
                ViewBag.Name = name;
                return View();
            }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
