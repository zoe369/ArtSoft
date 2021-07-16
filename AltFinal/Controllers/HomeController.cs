using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AltFinal.Controllers
{
    public class HomeController : Controller
    {
        // get dbconnection
        String ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult InsertFields(string FirstName, string LastName, int Mobno, string Adress)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CRUD_Operation(FirstName, LastName, Mobno, Adress) values ('" + FirstName + "', '" + LastName + "', '" + Mobno + "', '" + Adress + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();


                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ViewData1()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from CRUD_Operation", con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                string html = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html += "['" + dt.Rows[i]["id"] + "','" + dt.Rows[i]["FirstName"] + "','" + dt.Rows[i]["LastName"] + "','" + dt.Rows[i]["Mobno"] + "','" + dt.Rows[i]["Adress"] + "','<a class='margin_space'onclick]=fnEdit("+dt.Rows[i]["id"]+")><b>Edit</b></a>     /  <a onclick=fnDelete(" + dt.Rows[i]["id"]+")<b>Delete</b></a>'],";

                    }
                }
                con.Close();
                html = html.Substring(0, Math.Max(0, html.Length - 1));

                return Json(new { html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteData(string id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from CRUD_Operation where id='" + id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditData(string id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select FirstName, LastName, Mobno, Adress, id from CRUD_Operation where id='" + id + "'", con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                string[] htmlValues = new string[dt.Columns.Count];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        htmlValues[i] += dt.Rows[0].ItemArray[i].ToString();

                    }
                }
                con.Close();

                return Json(new { htmlValues }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateFields(string FirstName, string LastName, int Mobno, string Adress, int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("update CRUD_Operation set FirstName='"+FirstName+"', LastName ='"+LastName+"', Mobno='"+Mobno+"', Adress='"+Adress+"' where id='"+id+"'", con);
                cmd.ExecuteNonQuery();
                con.Close();


                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("failure", JsonRequestBehavior.AllowGet);
            }
        }
    }
}