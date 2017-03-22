using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2tima.Crypto
{
    public class RC4
    {
        byte[] S = new byte[256];

        int x = 0;
        int y = 0;
        /// <summary>
        /// Broken! Сломан!
        /// </summary>
        /// <param name="Key">Ключ шифования</param>
        public RC4(string Key)
        {
            byte[] key = Encoding.Default.GetBytes(Key);
            // init(key);

            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;
                S.Swap(i, j);
            }
        }


        public string Encode(string Data)
        {
            byte[] data = Encoding.Default.GetBytes(Data).Take(Data.Length).ToArray();
            byte[] cipher = new byte[data.Length];
            string _out = "";
            for (int m = 0; m < data.Length; m++)
            {
                _out += ((byte)(data[m] ^ keyItem())).ToString();
            }
            return _out;
        }
        public string Decode(string Data) => Encode(Data);

        // Pseudo-Random Generation Algorithm 
        // Генератор псевдослучайной последовательности 
        private byte keyItem()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            S.Swap(x, y);

            return S[(S[x] + S[y]) % 256];
        }
    }

    static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
