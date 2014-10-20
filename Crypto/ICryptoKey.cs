using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public interface ICryptoKey
    {
        void EncodeKeyFromString(string input);

        void DecodeKeyFromString(string input);

        string EncodeKeyToString();

        string DecodeKeyToString();
    }
}
