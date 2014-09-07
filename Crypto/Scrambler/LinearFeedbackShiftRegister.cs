namespace Crypto.Scrambler
{
    public class LinearFeedbackShiftRegister
    {
        /// <summary>
        /// Current value of register
        /// </summary>
        private uint _s;

        /// <summary>
        /// Feedback taps
        /// </summary>
        private readonly byte[] _taps = {31, 30, 29, 27, 25, 0};

        public LinearFeedbackShiftRegister(uint seed)
        {
            _s = seed;
        }

        /// <summary>
        /// Get next bit of LFSR
        /// </summary>
        /// <returns></returns>
        public bool NextBit()
        {
            uint newBit = _s >> _taps[0];
            for (int i = 1; i < _taps.Length; i++)
            {
                newBit ^= _s >> _taps[i];
            }
            newBit &= 1;

            _s = (_s >> 1) | (newBit << 31);

            return (_s & 1) == 1;
        }
    }
}
