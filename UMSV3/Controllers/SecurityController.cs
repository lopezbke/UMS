using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult Login( string shout)
        {
            ViewBag.Shout = shout;
            UserCred obj = new UserCred();
            return View(obj);
        }
        // POST: Security
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName, Password, Shout")] UserCred obj)
        {
            System.Diagnostics.Debug.WriteLine(obj.UserName);
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            System.Diagnostics.Debug.WriteLine(errors);
            if (ModelState.IsValid)
            {

                SqlConnection sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UMS"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("ConfirmUser", sqlconnection);
                sqlCommand.Parameters.AddWithValue("@UserName",obj.UserName);
                sqlCommand.Parameters.AddWithValue("@Password", obj.Password);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlconnection.Open();

                var reader = sqlCommand.ExecuteReader();
                try
                {
                    reader.Read();
                    /*System.Diagnostics.Debug.WriteLine(reader["UserName"]);
                    System.Diagnostics.Debug.WriteLine(reader["RoleId"]);
                    System.Diagnostics.Debug.WriteLine(reader["Password"]);
                    System.Diagnostics.Debug.WriteLine(reader["StatusId"]);*/
                    string userId = Convert.ToString(reader["UserId"]);
                    string username = Convert.ToString(reader["UserName"]);
                    string role = Convert.ToString(reader["RoleId"]);
                    string password = Convert.ToString(reader["Password"]);
                    string status = Convert.ToString(reader["StatusId"]);
                    if (status == "1")
                    {
                        FormsAuthentication.SetAuthCookie(userId, false);
                        Session.Add("UserId", userId);
                        FormsAuthentication.SetAuthCookie(username, false);
                        Session.Add("UserName", username);
                        FormsAuthentication.SetAuthCookie(password, false);
                        Session.Add("Password", password);
                        FormsAuthentication.SetAuthCookie(Convert.ToString(status), false);
                        Session.Add("Status", status);
                        FormsAuthentication.SetAuthCookie(Convert.ToString(role), false);
                        Session.Add("Role", role);


                        System.Diagnostics.Debug.WriteLine(Session["UserId"]);
                        System.Diagnostics.Debug.WriteLine(Session["Role"]);
                        System.Diagnostics.Debug.WriteLine(Session["Status"]);
                        System.Diagnostics.Debug.WriteLine(Session["Password"]);
                        System.Diagnostics.Debug.WriteLine(Session["UserName"]);

                        if (Session["Password"].ToString() == "Welcome12")
                        {
                            return RedirectToAction("InitialLogin");
                        }
                    }
                    else 
                    {
                        System.Diagnostics.Debug.WriteLine(Session["Status"]);
                        string shout = "Account is currently inactive, please contact admin at admin@UMS.com ";
                        return RedirectToAction("Login", new { shout = shout });

                    }
                   
                }
                catch 
                {
                    System.Diagnostics.Debug.WriteLine("No combination of that UserName and Password found.");
                    string shout = "Invalid Credentials, please try again.";
                    /*ViewBag.Shout = "No combination of that UserName and Password found.";*/
                    return RedirectToAction("Login", new { shout = shout });
                }
              

            }

            /*var isUserName = db.UserInfoes.Any(x => x.UserName == obj.UserName);
        var isPassword = db.UserCredentials.Any(y => y.Password == obj.Password);*/
            /* string query1 = "SELECT TOP 1 UserId FROM UserInfo WHERE UserName = @userName";
             string query2 = "SELECT TOP 1 Password FROM UserCredentials WHERE UserId = @userId";
             var userId = db.UserInfoes.SqlQuery(query1, new SqlParameter("@userName",obj.UserName));
             var isPassword = db.UserCredentials.SqlQuery(query2, new SqlParameter("@userId", userId));
             System.Diagnostics.Debug.WriteLine(userId);
             System.Diagnostics.Debug.WriteLine(isPassword);
*/
            return RedirectToAction("Index", "userinfo");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult InitialLogin() 
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            PasswordReset obj = new PasswordReset();
            return View(obj);
        }
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult InitialLogin([Bind(Include ="UserName,OldPassword, Password, ConfirmPassword,Name,Email")] PasswordReset obj) 
        {
            if (ModelState.IsValid)
            {
                 
                FormsAuthentication.SignOut();
                Session.Clear();
                SqlConnection sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UMS"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("NewPassword", sqlconnection);
                sqlCommand.Parameters.AddWithValue("@UserName", obj.UserName);
                sqlCommand.Parameters.AddWithValue("@Password", obj.ConfirmPassword);
                sqlCommand.Parameters.AddWithValue("@Email", obj.Email);
                sqlCommand.Parameters.AddWithValue("@FirstName", obj.Name);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlconnection.Open();

                var reader = sqlCommand.ExecuteReader();
                reader.Read();
            
                return RedirectToAction("Logout");
            }
            return View();
        }
        public ActionResult SendPasswordEmail() 
        {
            return View();
        }
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult SendPasswordEmail([Bind(Include ="UserName,Name,Email,Name,Email ")] EmailPasswordReset obj )
        {
            if (ModelState.IsValid) 
            {
                
                SqlConnection sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UMS"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("NewPassword", sqlconnection);
                SqlCommand sqlCommand2 = new SqlCommand("EmailIntoDb", sqlconnection);
                
                //Password Reset: Where UserName, FirstName and Email matches the input given.
                sqlCommand.Parameters.AddWithValue("@UserName", obj.UserName);
                sqlCommand.Parameters.AddWithValue("@Password", "Welcome12");
                sqlCommand.Parameters.AddWithValue("@Email", obj.Email);
                sqlCommand.Parameters.AddWithValue("@FirstName", obj.Name);
                //Adds a new row to the EmailsTable with the parameters given.
                sqlCommand2.Parameters.AddWithValue("@UserName", obj.UserName);
                sqlCommand2.Parameters.AddWithValue("@Name", obj.Name);
                sqlCommand2.Parameters.AddWithValue("@Email", obj.Email);
                sqlCommand2.Parameters.AddWithValue("@bool", false);


                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand2.CommandType = CommandType.StoredProcedure;

                sqlconnection.Open();
                sqlCommand.ExecuteReader();
                sqlconnection.Close();

                sqlconnection.Open();
                sqlCommand2.ExecuteReader();
                sqlconnection.Close();
                
                return RedirectToAction("Logout");
            }

            return View();
        }
    }
}