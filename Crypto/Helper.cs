using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public static class Helper
    {
        public static string ToCharString(this char[] array)
        {
            return new String(array);
        }
    }
}
