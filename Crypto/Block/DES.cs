using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Block
{
    public class Des : ICryptoCoder
    {
        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            _feistelCoder.Key = Key;
            return _feistelCoder.Encode(message);
        }

        public byte[] Decode(byte[] message)
        {
            _feistelCoder.Key = Key;
            return _feistelCoder.Decode(message);
        }

        #endregion ICryptoCoder

        private readonly Feistel _feistelCoder;

        public Des()
        {
            _feistelCoder = new Feistel();
        }

        //private 
    }
}
