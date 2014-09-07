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

        string Encode(string message);

        string Decode(string message);
    }
}
