using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenceApi.Models
{
    public class LicenceTable
    {

        public int Id { get; set; }
        public string Licensekey { get; set; }
        public string ExpirationDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public bool IsDelete { get; set; }
        public string DeleteDate { get; set; }
    }
}