using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseAPI.PairGenerator
{
    class Program
    {
        /*
         Pass    : Blue Byte Systems, Inc.
         Private : MHcwIwYKKoZIhvcNAQwBAzAVBBBicgsQh07gerhOiRUYUPnDAgEKBFB1vfxoYkihhwUDTZJ0UGJ86320WsTIVFyRLngD151KbN5kKffFTCMrGsRKo3kl3zvT1lZI+nLFMAxYwqzfJQsyqkEZwv4J3KKoIk6UeqqgeA==
         Public  : MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwZq2xT4eBZQVgNATKNs8KwwCtscuv2QsiBiunMBZ/CRinlh7aluD1WfZodEHQdpv1oLTnBzLxDbL+I2dAqr/pQ==
         */

        static void Main(string[] args)
        {
            var passPhrase = "Blue Byte Systems, Inc.";
            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            var publicKey = keyPair.ToPublicKeyString();



            var desktop = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var licenseFile = System.IO.Path.Combine(desktop, "TestLicense.xml");

            var licenseManager = new LicenseAPI.Business.LicenseManager();

            licenseManager.PassPhrase = "Blue Byte Systems, Inc.";

            licenseManager.PublicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwZq2xT4eBZQVgNATKNs8KwwCtscuv2QsiBiunMBZ/CRinlh7aluD1WfZodEHQdpv1oLTnBzLxDbL+I2dAqr/pQ==";
            licenseManager.PrivateKey = "MHcwIwYKKoZIhvcNAQwBAzAVBBBicgsQh07gerhOiRUYUPnDAgEKBFB1vfxoYkihhwUDTZJ0UGJ86320WsTIVFyRLngD151KbN5kKffFTCMrGsRKo3kl3zvT1lZI+nLFMAxYwqzfJQsyqkEZwv4J3KKoIk6UeqqgeA==";

            licenseManager.New("A SOLIDWORKS Reseller", "dev@solidworksreseller.com", DateTime.Now.AddYears(1), licenseManager.PassPhrase, licenseFile);

            System.Console.ReadLine();
        }
    }
}
