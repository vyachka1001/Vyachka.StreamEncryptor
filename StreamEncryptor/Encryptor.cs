using EncryptorProj;
using System;
using System.Collections;

namespace EncryptorProj
{
    public class Encryptor
    {
        public int[] ToXor { get; } =
        {
            40, 21, 19, 2
        };

        public BitArray GeneratedKey { get; private set; }

        public BitArray Encrypt(BitArray key, BitArray data)
        {
            Console.WriteLine("Key: {0}", key.ToBitString());
            GeneratedKey = GenerateKey(key, data.Count);
            Console.WriteLine("Generated key: {0}", GeneratedKey.ToBitString());
            return data.Xor(GeneratedKey);
        }

        private BitArray GenerateKey(BitArray key, int requiredLen)
        {
            LSFR register = new LSFR(key, ToXor);
            BitArray generatedKey = new BitArray(requiredLen);
            for (int i = requiredLen - 1; i >= 0; i--)
            {
                generatedKey[i] = register.GetNext();
            }

            Console.WriteLine(register.Counter);
            return generatedKey;
        }
    }
}
