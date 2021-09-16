using LicenceApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LicenseAPI.Business;

namespace LicenceApi.Controllers
{

    [RoutePrefix("api/LicenceData")]
    public class LicenceController : ApiController
    {

        public static String ServerName = "";
        public static String Database = "";
        public static String DefualtUserID = "";
        public static String DefualtPassword = "";

        SqlConnection con = new SqlConnection(GetConnectionString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp = null;

        public static String GetConnectionString()
        {
            ServerName = ConfigurationManager.AppSettings["ServerName"];
            Database = ConfigurationManager.AppSettings["Database"];
            DefualtUserID = ConfigurationManager.AppSettings["DefualtUserID"];
            DefualtPassword = ConfigurationManager.AppSettings["DefualtPassword"];
            return "Data Source=" + ServerName + ";Initial Catalog=" + Database +
                  ";User ID=" + DefualtUserID + ";Password=" + DefualtPassword + " ;Integrated Security=True;";

            //return "Data Source=" + ServerName + ";Initial Catalog=" + Database +
            //      ";User ID=" + DefualtUserID + ";Password=" + DefualtPassword + " ;Integrated Security=False;Connect Timeout=15;Encrypt=False;Packet Size=4096";

        }


        [HttpGet]
        [Route("getcicencedata")]
        public string GetLicenceData()
        {
            string result = string.Empty;
            try
            {
                DataTable CustomerList = new DataTable();

                using (var cmd = new SqlCommand("GetAllLicenceData", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(CustomerList);
                    result = JsonConvert.SerializeObject(CustomerList);
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return result;
        }


        [HttpPost]
        [Route("createlicence")]
        public string CreateLicence(LicenceTable LicModel)
        {
            string result = string.Empty;
            try
            {
                LicenseManager Createobj = new LicenseManager();
                string passPhrase = "Blue Byte Systems, Inc.";
                ////Assign LicenceKey Logic here
                DateTime expirydate = Convert.ToDateTime(LicModel.ExpirationDate);
                LicModel.Licensekey = Createobj.New(LicModel.CustomerName, LicModel.CustomerEmail, expirydate, passPhrase, "");
                ////Assign LicenceKey Logic here
                using (SqlCommand cmd = new SqlCommand("CreateNewLicence", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExpirationDate", LicModel.ExpirationDate);
                    cmd.Parameters.AddWithValue("@CustomerName", LicModel.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerEmail", LicModel.CustomerEmail);
                    cmd.Parameters.AddWithValue("@Licensekey", LicModel.Licensekey);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr["Id"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return result;
        }


        [HttpPost]
        [Route("deletelicence")]
        public string DeleteLicence(LicenceTable LicModel)
        {
            string result = string.Empty;
            try
            {

                using (SqlCommand cmd = new SqlCommand("DeleteLicence", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", LicModel.Id);
                    cmd.Parameters.AddWithValue("@Deletedate", LicModel.DeleteDate);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr["Result"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return result;
        }

    }


}
