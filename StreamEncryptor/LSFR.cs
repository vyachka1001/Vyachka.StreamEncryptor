using System.Collections;

namespace EncryptorProj
{
    public class LSFR
    {
        public int Counter { get; private set; }

        private int[] _toXor;
        public int[] ToXor
        {
            get => _toXor;
            set => _toXor = value;
        }

        private BitArray _data;
        private BitArray Data
        {
            get => _data;
            set => _data = value;
        }

        public LSFR(BitArray initializeData, int[] toXor)
        {
            ToXor = toXor;
            Data = new BitArray(initializeData);
        }

        private void Iterate()
        {
            bool lastElement = Data[ToXor[0] - 1];
            for (int i = 1; i < ToXor.Length; i++)
            {
                lastElement ^= Data[ToXor[i] - 1];
            }

            Data.LeftShift(1);
            Data[0] = lastElement;
        }

        public bool GetNext()
        {
            Counter++;
            bool result = Data[Data.Count - 1];
            Iterate();

            return result;
        }
    }
}
