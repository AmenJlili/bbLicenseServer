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

        public int SerchName { get; set; }

        public string Serachstring { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }

        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }

        public string XMLFormat { get; set; }
    }
}