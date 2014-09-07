using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public interface ICryptoCoder
    {
        string Encode(string message, object key);

        string Decode(string message, object key);
    }
}
