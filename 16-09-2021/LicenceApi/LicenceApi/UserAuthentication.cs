using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenceApi
{
    public class UserAuthentication : IDisposable
    {
        public string ValidateUser(string username, string password)
        {
            string Name = username == "SuperAdmin" ? "Valid" : "InValid";
            string Pass = password == "SuperAdmin" ? "Valid" : "InValid";

            if (Name == "Valid" && Pass == "Valid")
                return "true";
            else
                return "false";
        }
        public void Dispose()
        {
            //Dispose();
        }


    }
}