using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public interface ICryptoCoder
    {
        string Key { get; set; }

        byte[] Encode(byte[] message);

        byte[] Decode(byte[] message);

        ICryptoKey CryptoKey { get; }

        void GenerateCryptoKey();
    }
}
