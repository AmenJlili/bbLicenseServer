using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILicenceGenration
{
    public class LicenseManager
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string PassPhrase { get; set; }
        public string NewLicence(string CustomerName, string CustomerEmail, DateTime? Expiry, string PrivateKey)
        {
            string licenseMes = string.Empty;
            try
            {
                string passPhrase = CustomerName;
                DateTime expdate = Convert.ToDateTime(Expiry);

                var license = License.New().WithUniqueIdentifier(Guid.NewGuid()).As(LicenseType.Standard).ExpiresAt(expdate)
         .LicensedTo(CustomerName, CustomerEmail)
        .CreateAndSignWithPrivateKey(PrivateKey, passPhrase);

                // File.WriteAllText(licensePath, license.ToString(), Encoding.UTF8);
                licenseMes = license.ToString();
            }
            catch (Exception ex)
            {
                licenseMes = ex.InnerException.ToString();
            }
            return licenseMes;

        }

        public string Newwithoutexpiry(string CustomerName, string CustomerEmail, string PrivateKey)
        {
            string licenseMes = string.Empty;
            try
            {
                string passPhrase = CustomerName;

                var license = License.New().WithUniqueIdentifier(Guid.NewGuid()).As(LicenseType.Standard)
         .LicensedTo(CustomerName, CustomerEmail)
        .CreateAndSignWithPrivateKey(PrivateKey, passPhrase);

                //   File.WriteAllText(licensePath, license.ToString(), Encoding.UTF8);
                licenseMes = license.ToString();
            }
            catch (Exception ex)
            {
                licenseMes = ex.InnerException.ToString();
            }
            return licenseMes; ;

        }

        public string LicencGenerate(string passPhrase)
        {
            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            PrivateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            //publicKey = keyPair.ToPublicKeyString();

            return PrivateKey;
        }

        public string GenerateKeys(string CustomerName, string CustomerEmail, string PrivateKey, DateTime? Expiry)
        {
            string Keys = string.Empty;
            try
            {
                var passPhrase = CustomerName;//"Blue Byte Systems, Inc."; ///Customer Name from web app
                var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
                var keyPair = keyGenerator.GenerateKeyPair();
                //var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);///When licence created this key generated 
                var privatekey = PrivateKey; ///Customer Name from web app
                var publicKey = keyPair.ToPublicKeyString();
                // string fullSavePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/App_Data/"));


                PassPhrase = passPhrase; // Pass customer name 

                // PublicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwZq2xT4eBZQVgNATKNs8KwwCtscuv2QsiBiunMBZ/CRinlh7aluD1WfZodEHQdpv1oLTnBzLxDbL+I2dAqr/pQ==";
                // PrivateKey = "MHcwIwYKKoZIhvcNAQwBAzAVBBBicgsQh07gerhOiRUYUPnDAgEKBFB1vfxoYkihhwUDTZJ0UGJ86320WsTIVFyRLngD151KbN5kKffFTCMrGsRKo3kl3zvT1lZI+nLFMAxYwqzfJQsyqkEZwv4J3KKoIk6UeqqgeA==";
                PublicKey = publicKey;
                PrivateKey = privatekey;
                if (Expiry != null)
                    Keys = NewLicence(CustomerName, CustomerEmail, Expiry, PrivateKey);
                else
                    Keys = Newwithoutexpiry(CustomerName, CustomerEmail, PrivateKey);

            }
            catch (Exception ex)
            {
                Keys = ex.InnerException.ToString();
            }


            return Keys;
        }

    }
}
