using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMSV3.Controllers
{
    public class PictureController : Controller
    {
        // GET: Picture
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
        public ActionResult PictureAction(int id)
        {
             var a = GetImage(id);
            return RedirectToAction("Edit","UserInfo", new {a = a });
        }
    }
}