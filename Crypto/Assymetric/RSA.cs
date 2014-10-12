using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;

namespace Crypto.Assymetric
{
    public class RSA1
    {
        private void Foo()
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa =
                new System.Security.Cryptography.RSACryptoServiceProvider();
            var rng = RandomNumberGenerator.Create();

        }
    }
}
