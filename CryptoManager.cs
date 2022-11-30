using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;

namespace CW_RC5
{
    public class RC5CryptoManager
    {

        public bool IsPassPhraseCorrect = false;
        private ulong[] S;
        private ulong[] L;
        private int u;
        private int c;
        private int b;
        private BigInteger mod2w;
        private readonly int w = 64;   
        private readonly int R = 0;//10;              //10
        private readonly ulong P64 = 0XB7E151628AED2A6B;
        private readonly ulong Q64 = 0X9E3779B97F4A7C15;

        public byte[] Decrypt(byte[] data, string password)
        {
            // set extract key to private field key
            GenerateKey(password);
            byte[] key = Encoding.Unicode.GetBytes(password);
            //key = AlignArray(key, u);
            byte[] datakey = new byte[key.Length];
            data = AlignArray(data, 16);
            byte[] res = new byte[data.Length];
            byte[] input = new byte[16];

            for (int i = 0; i < data.Length / 16; i++)
            {
                Array.Copy(data, i * 16, input, 0, 16);
                Array.Copy(DecryptBlock(input), 0, res, i * 16, 16);
            }
            Array.Copy(res, 0, datakey, 0, key.Length);
            byte[] result = new byte[res.Length- key.Length];
            Array.Copy(res, key.Length, result, 0, res.Length - key.Length);

            IsPassPhraseCorrect = datakey.SequenceEqual(key);//Array.Equals(datakey, key);// datakey == key;

            return result;
        }
        public byte[] AlignArray(byte[] bytes, int m)
        {
           
            byte[] buf;
            int padding = (int)(bytes.Length % m);
            if (padding != 0)
            {
                padding = m - padding;
                
                buf = new byte[padding + bytes.Length];
                Array.Copy(bytes, 0, buf, 0, bytes.Length);
                return buf;
            }
            return bytes;

        }
        public byte[] DataWithKey(byte[] data, string pass)
        {
            byte[] key = Encoding.Unicode.GetBytes(pass);
            byte[] data_with_key = new byte[data.Length + key.Length];
            Array.Copy(key, 0, data_with_key, 0, key.Length);
            Array.Copy(data, 0, data_with_key, key.Length, data.Length);
            return data_with_key;
        }
        public byte[] Encrypt(byte[] data, string password)
        {
            GenerateKey(password);

            byte[] buf = AlignArray(DataWithKey(data, password),16);
            byte[] res = new byte[buf.Length];
            byte[] input = new byte[16];
           
            for (int i = 0; i < buf.Length / 16; i++)
            {
                Array.Copy(buf, i * 16, input, 0, 16);
                Array.Copy(EncryptBlock(input), 0, res, i * 16, 16);
            }
            return res;
        }

        public bool[] CheckPasswordPhrase(string key, bool includeDigit, bool includeLowerCase, bool includeUpperCase, bool includeSpecChar)
        {
            bool[] res = new bool[4];

            res[0] = !includeDigit || key.Any(ch => !char.IsDigit(ch));
            res[1] = !includeLowerCase || key.Any(ch => !char.IsLower(ch));
            res[2] = !includeUpperCase || key.Any(ch => !char.IsUpper(ch));
            res[3] = !includeSpecChar || key.Any(ch => !char.IsLetterOrDigit(ch));

            if (res[0] && res[1] && res[2] && res[3]) return null;
            return res;
        }
        
        public void GenerateKey(string key)
        {
            //Константы как поля класса
            mod2w = new BigInteger(Math.Pow(2, 64));
            
            byte[] K = Encoding.Unicode.GetBytes(key);
            b =  K.Length;
            
            //Разбиение ключа на слова
            u = w >> 3;                                // количество байт в слове
            
            // allign key
            if (b == 0) 
                c = 1;
            else if (b % u != 0) {
                K = AlignArray(K, u);
                b = K.Length;
                c = b / u;
            } 
            else c = b / u;                                 // количество слов в ключе

            L = new ulong[c];
            int i, j;
            for (i = b - 1; i >= 0; i--)
                //L[i / u] = ROTL(L[i / u], 8) + K[i];
                L[i / u] = (ulong)((L[i / u] << 8) + K[i]) ;//% Math.Pow(2, w));
            
            //Построение таблицы расширенных ключей
            int t = 2 * (R + 1);
            S = new ulong[t];
            S[0] = P64;
            for (i = 1; i < t; i++)//-------------------------------------
                S[i] = (ulong)((S[i - 1] + Q64) % Math.Pow(2, w));
            
        
            //Перемешивание
            ulong A, B;
            
            A = B = 0;
            i = j = 0;
           
            for (int k = 0; k < 3 * Math.Max(t,c) ; k++)
            {
                A = S[i] = ROTL((ulong)((S[i] + A + B) % Math.Pow(2,w)), 3);                                                            //ROTL(S[i] + A + B, 3);               
                B = L[j] = ROTL((ulong)((L[j] + A + B) % Math.Pow(2, w)), (int)((S[i] + A + B) % Math.Pow(2, w)));                                  // ROTL(L[j] + A + B, (int)(A + B));    
                i = (i + 1) % t;
                j = (j + 1) % c;
            }
        }
        
        public ulong ROTL(ulong x, int offset)
        {
            //return x << (offset & (Math.w - 1)) | x >> (w - (offset & (w - 1)));
            //return x << offset | x >> w - offset;
            offset %= w;
            return ((x << offset) & (ulong)(Math.Pow(2, w) - 1)) | ((x & (ulong)(Math.Pow(2, w) - 1)) >> (w - offset));
        }
        public ulong ROTR(ulong x, int offset)
        {
            //return x >> (offset & (w - 1)) | x << (w - (offset & (w - 1)));(ulong)Math.Pow(2,w)
            //return x >> offset  | x << w - offset;
            offset %= w;
            return ((x & (ulong)(Math.Pow(2, w) - 1)) >> offset) | (x << (w - offset) & (ulong)(Math.Pow(2, w) - 1));
        }
        //абросимова, вавилов, желтиков, калмыкова, каменев, поздеев, степанов,харитоненкова, щемилкин, коротаев
        
        public byte[] EncryptBlock(byte[] block128bit)
        {
            ulong[] I = BytesToUInt64(block128bit);

            // A, B - two input w-bit(64bit) register
            
            I[0] = (ulong)((I[0] + S[0]) % mod2w);
            I[1] = (ulong)((I[1] + S[1]) % mod2w); 

            for (int i = 1; i <= R ; i++)
            {
                I[0] = (ulong)((ROTL(I[0] ^ I[1], (int)I[1]) + S[2 * i]) % mod2w);        //I[0] = ROTL(I[0] ^ I[1], (int)I[1]) + S[2 * i];          
                I[1] = (ulong)((ROTL(I[0] ^ I[1], (int)I[0]) + S[2 * i + 1]) % mod2w);        //I[1] = ROTL(I[0] ^ I[1], (int)I[0]) + S[2 * i + 1];      
            }

            return UInt64ToBytes(I);
        }
        public byte[] DecryptBlock(byte[] block128bit)
        {
            ulong[] I = BytesToUInt64(block128bit);

            for (int i = R; i >= 1; i--)
            {
                I[1] = ROTR((I[1] - S[2 * i + 1]), (int)I[0]) ^ I[0];
                I[0] = ROTR((I[0] - S[2 * i]), (int)I[1]) ^ I[1];
            }

            I[1] = (ulong)((I[1] - S[1]) % mod2w);
            I[0] = (ulong)((I[0] - S[0]) % mod2w);

            return UInt64ToBytes(I);
        }

        public byte[] UInt64ToBytes(ulong[] I)
        {
            byte[] res = new byte[I.Length * 8];
            for (int i = 0; i < I.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(I[i]), 0, res, i*8 , 8);
            }
            return res; 
        }

        public ulong[] BytesToUInt64(byte[] bytes)
        {
            byte[] src = AlignArray(bytes,16);
            ulong[] res = new ulong[src.Length/8];
            byte[] block64bit = new byte[8];

            for (int i = 0; i < src.Length / 8; i++)
            {
                Array.Copy(src, i*8, block64bit, 0, 8);
                res[i] = BitConverter.ToUInt64(block64bit, 0);
            }
            return res;
        }
    }
}


/*const int w = 0b1000000;         //64
            const int R = 0b1010;              //10
            const ulong P64 = 0XB7E151628AED2A6B;
            const ulong Q64 = 0X9E3779B97F4A7C15;*/

/*(string passPhrase)
       {
           if (passPhrase == this.key) return true;
           return false;
       }*/

/*byte[] A = BitConverter.GetBytes(I[0]);
            byte[] В = BitConverter.GetBytes(I[1]);*/
/*System.Buffer.BlockCopy(BitConverter.GetBytes(I[0]), 0, res, 0, 8)
System.Buffer.BlockCopy(BitConverter.GetBytes(I[1]), 0, res, 8, 8);*/
/*I[0] = BitConverter.ToUInt64(block128bit, 0);
I[1] = BitConverter.ToUInt64(block128bit, 8);*/
// byte[] paddingBuf;
//paddingBuf = new byte[padding];
//Array.Copy(paddingBuf, 0, buf, bytes.Length, padding);
/*buf = new byte[bytes.Length];
Array.Copy(bytes, buf, bytes.Length);*/

//int bytesLength = BitConverter.ToInt32(result, 0);
//return result.Take(bytesLength).ToArray();

//byte[] buf = AlignArray(DataWithKey(data),16);