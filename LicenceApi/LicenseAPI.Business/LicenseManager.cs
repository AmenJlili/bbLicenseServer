using Standard.Licensing;
using System;
using System.IO;
using System.Text;

namespace LicenseAPI.Business
{
    public class LicenseManager
    {
        public string PrivateKey { get; set; }
        public string PassPhrase { get; set; }

        public string PublicKey { get; set; }
        public LicenseManager()
        {
             
        }

        public void New(bbLicense bblicense, string passPhrase, string licensePath)
        {
            var license = License.New()
    .WithUniqueIdentifier(Guid.NewGuid())
    .As(LicenseType.Standard)
    .ExpiresAt(bblicense.Expiry)
    .LicensedTo($"{bblicense.CustomerName} {bblicense.CustomerLastName}", bblicense.CustomerEmail)
    .CreateAndSignWithPrivateKey(PrivateKey, passPhrase);

        File.WriteAllText(licensePath, license.ToString(), Encoding.UTF8);
        }
    }

    public class bbLicense
    {
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
     
        public string CustomerEmail { get; set; }

        public DateTime Expiry { get; set; }
    }


}
