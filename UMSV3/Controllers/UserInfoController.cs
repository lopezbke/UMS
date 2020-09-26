using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UMSV3.Models;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Ajax.Utilities;
using System.IO;
using System.Drawing;

namespace UMSV3.Controllers
{
    [Authorize]
    public class UserInfoController : Controller
    {
        private UMSEntities db = new UMSEntities();

        public String GetImage(int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UMS"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("GetFromImageBank", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            sqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            reader.Read();

            var image = reader["ImageData"];
            var imageType = reader["ImageType"];
            byte[] imageAsBytes = (byte[])image;

            /* for (int i = 0; i < image2.Length; i++)
             {

                 System.Diagnostics.Debug.WriteLine(image2[i]);

             }*/
            var a = System.Convert.ToBase64String(imageAsBytes);
            System.Diagnostics.Debug.WriteLine(a);
            sqlConnection.Close();


            return a;
        }
        // GET: UserInfo
        public ActionResult Index()
        {
            var userInfoes = db.UserInfoes.Include(u => u.Role).Include(u => u.Status).Include(u => u.UserCredential);
            
            return View(userInfoes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string obj)
        {
            var userInfoes = db.UserInfoes.Include(u => u.Role).Include(u => u.Status).Include(u => u.UserCredential).Where(
                u => u.FirstName == obj || u.UserName == obj || u.LastName == obj || u.Email == obj || u.C_Address == obj || u.City == obj || u.Country == obj
                || u.PhoneNumber == obj );
            return View(userInfoes);
        }
        private static Image BinaryToImage(byte[] binaryData)
        {
            MemoryStream ms = new MemoryStream(binaryData);
            Image img = Image.FromStream(ms);
            return img;
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
           
            /*ImageConverter converter = new ImageConverter();
            ViewBag.Image = (byte[])converter.ConvertTo(x, typeof(byte[]));*/
            /*userInfo.imageBuffer= (byte[])converter.ConvertTo(x, typeof(byte[]));*/

            /*System.Diagnostics.Debug.WriteLine(x);*/
           /* BinaryToImage(x);*/
            
            return View(userInfo);
        }

      

        // GET: UserInfo/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["Role"]) == "2")
            {
                return RedirectToAction("Index");
            }
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
        public ActionResult Create([Bind(Include = "UserId,UserName,FirstName,LastName,Email,C_Address,City,Country,ZipCode,PhoneNumber,StatusId,RoleId,Password")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfoes.Add(userInfo);
                db.SaveChanges();
                var emailEmail = userInfo.Email;
                var emailFirstName = userInfo.FirstName;
                var emailUserName = userInfo.UserName;
                var credentialUserId = userInfo.UserId;

                return RedirectToAction("SendEmail", new { email = emailEmail, name = emailFirstName, userName = emailUserName, userId = credentialUserId });
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userInfo.RoleId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName", userInfo.StatusId);
            ViewBag.UserId = new SelectList(db.UserCredentials, "UserId", "Password", userInfo.UserId);
            return View(userInfo);
        }

        public ActionResult SendEmail(string email, string name, string userName, int  userId)
        {
            var credentialUserId = userId;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UMS"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("EmailIntoDb", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            //sqlCommand.Parameters.
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@Email", email);
            sqlCommand.Parameters.AddWithValue("@UserName", userName);
            sqlCommand.Parameters.AddWithValue("@bool", false);
            sqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            /* Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
             Microsoft.Office.Interop.Outlook.MailItem mailItem = (Microsoft.Office.Interop.Outlook.MailItem)application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
             mailItem.To = email;
             mailItem.Subject = "Setup Password";
             mailItem.HTMLBody = "Hello " + name + "," + "<br>" + "<br>" + "Your login username is: " + userName + "<br>" + "Please visit the link below to create a new password:" + "<br>" + $"<a href='https:localhost:44341/Security/NewPassword?name={name}'> Setup Password</a>";
             mailItem.Send();
             string a = email;
             System.Diagnostics.Debug.WriteLine(email);*/
            sqlConnection.Close();
            return RedirectToAction("AddUserCredential", "UserCredentials", new {userId =  credentialUserId});
        }

        // GET: UserInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            string b = GetImage(id.Value);
            ViewBag.Image = b;
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
        public ActionResult Edit([Bind(Include = "UserId,UserName,FirstName,LastName,Email,C_Address,City,Country,ZipCode,PhoneNumber,StatusId,RoleId, fileUpload")] UserInfo userInfo, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();

                //Image Upload
                System.Diagnostics.Debug.WriteLine(fileUpload.FileName);
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UMS"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("SaveImage", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                //sqlCommand.Parameters.
                sqlCommand.Parameters.AddWithValue("@UserId",userInfo.UserId);
                sqlCommand.Parameters.AddWithValue("@ImageData", fileUpload.InputStream);
                sqlConnection.Open();
                sqlCommand.ExecuteReader();
                sqlConnection.Close();

            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", userInfo.RoleId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName", userInfo.StatusId);
            ViewBag.UserId = new SelectList(db.UserCredentials, "UserId", "Password", userInfo.UserId);

            return RedirectToAction("Index");
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
            if (id == Convert.ToInt32(Session["UserId"])) 
            {
               
                return RedirectToAction("Index");
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
