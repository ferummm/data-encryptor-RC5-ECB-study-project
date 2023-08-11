using System;
using System.Linq;
using System.IO;

namespace CW_RC5
{
    public class RC5CryptoManager
    {
        public bool IsPassPhraseCorrect = false;
        private readonly int w = 64;
        private readonly int R = 16;

        private const UInt64 Pw = 0xB7E151628AED2A6B;
        private const UInt64 Qw = 0x9E3779B97F4A7C15;

        private UInt64[] L;
        private UInt64[] S;
        private int t;
        private int b;
        private int u;
        private int c;

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            GenerateKey(key);

            byte[] datakey = new byte[key.Length];
            byte[] res = new byte[data.Length];
            byte[] input = new byte[16];

            for (int i = 0; i < data.Length / 16; i++)
            {
                Array.Copy(data, i * 16, input, 0, 16);
                Array.Copy(DecryptBlock(input), 0, res, i * 16, 16);
            }

            byte[] result;
            try
            {
                Array.Copy(res, 4, datakey, 0, key.Length);
                result = new byte[BitConverter.ToInt32(res, 0) - key.Length];
                Array.Copy(res, key.Length + 4, result, 0, BitConverter.ToInt32(res, 0) - key.Length);
            }
            catch (Exception)
            {
                throw new ArgumentException("Неверная парольная фраза.");
            }
            IsPassPhraseCorrect = datakey.SequenceEqual(key);
            return result;
        }
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            GenerateKey(key);
            byte[] buf;
            buf = AllignData(DataWithKey(data, key), 16);
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

            res[0] = !includeDigit || key.Any(ch => char.IsDigit(ch));
            res[1] = !includeLowerCase || key.Any(ch => char.IsLower(ch));
            res[2] = !includeUpperCase || key.Any(ch => char.IsUpper(ch));
            res[3] = !includeSpecChar || key.Any(ch => !char.IsLetterOrDigit(ch));

            if (res[0] && res[1] && res[2] && res[3]) return null;
            return res;
        }

        private void GenerateKey (byte[] key)
        {
            u = w >> 3;
            b = key.Length;
            c = b % u > 0 ? b / u + 1 : b / u;
            KeyToWords(key);
            ExtendKey();
            Mixing();
        }

        private void KeyToWords(byte[] key)
        {
            int i;
            L = new UInt64[c];

            for (i = b - 1; i >= 0; i--)
            {
                L[i / u] = ROTL(L[i / u], 8) + key[i];
            }
        }

        private void ExtendKey()
        {
            int i;
            t = 2 * (R + 1);
            S = new UInt64[t];
            S[0] = Pw;
            for (i = 1; i < t; i++)
            {
                S[i] = S[i - 1] + Qw;
            }
        }

        private void Mixing()
        {
            UInt64 x, y;
            int i, j, n;
            x = y = 0;
            i = j = 0;
            n = 3 * Math.Max(t, c);

            for (int k = 0; k < n; k++)
            {
                x = S[i] = ROTL((S[i] + x + y), 3);
                y = L[j] = ROTL((L[j] + x + y), (int)(x + y));
                i = (i + 1) % t;
                j = (j + 1) % c;
            }
        }

        private UInt64 ROTL(UInt64 a, int offset)
        {
            return (a << offset) | (a >> (w - offset));
        }
        
        private UInt64 ROTR(UInt64 a, int offset)
        {
            return (a >> offset) | (a << (w - offset));
        }
        
        private UInt64 BytesToUInt64(byte[] b, int p)
        {
            UInt64 r = 0;
            for (int i = p + 7; i > p; i--)
            {
                r |= (UInt64)b[i];
                r <<= 8;
            }
            r |= (UInt64)b[p];
            return r;
        }
       
        private void UInt64ToBytes(UInt64 a, byte[] b, int p)
        {
            for (int i = 0; i < 7; i++)
            {
                b[p + i] = (byte)(a & 0xFF);
                a >>= 8;
            }
            b[p + 7] = (byte)(a & 0xFF);
        }

        private byte[] EncryptBlock(byte[] inBuf)
        {
            UInt64 a = BytesToUInt64(inBuf, 0);
            UInt64 b = BytesToUInt64(inBuf, 8);

            a += S[0];
            b += S[1];

            for (int i = 1; i < R + 1; i++)
            {
                a = ROTL((a ^ b), (int)b) + S[2 * i];
                b = ROTL((b ^ a), (int)a) + S[2 * i + 1];
            }
            byte[] outBuf = new byte[16];
            UInt64ToBytes(a, outBuf, 0);
            UInt64ToBytes(b, outBuf, 8);
            return outBuf;
        }

        private byte[] DecryptBlock(byte[] inBuf)
        {
            UInt64 a = BytesToUInt64(inBuf, 0);
            UInt64 b = BytesToUInt64(inBuf, 8);

            for (int i = R; i > 0; i--)
            {
                b = ROTR((b - S[2 * i + 1]), (int)a) ^ a;
                a = ROTR((a - S[2 * i]), (int)b) ^ b;
            }

            b -= S[1];
            a -= S[0];

            byte[] outBuf = new byte[16];
            UInt64ToBytes(a, outBuf, 0);
            UInt64ToBytes(b, outBuf, 8);
            return outBuf;
        }
        
        private byte[] AllignData(byte[] data, int m)
        {
            byte[] res;
            using (var mstream = new MemoryStream())
            using (var writer = new BinaryWriter(mstream))
            {
                writer.Write(data.Length);
                writer.Write(data);

                int padding = (int)(mstream.Length % m);
                if (padding != 0)
                {
                    padding = m - padding;
                    var rng = System.Security.Cryptography.RNGCryptoServiceProvider.Create();
                    byte[] paddingBuf = new byte[padding];
                    rng.GetNonZeroBytes(paddingBuf);
                    writer.Write(paddingBuf);
                }
                res = mstream.ToArray();
            }

            return res;
        }

        private byte[] DataWithKey(byte[] data, byte[] key)
        {
            byte[] data_with_key = new byte[data.Length + key.Length];
            Array.Copy(key, 0, data_with_key, 0, key.Length);
            Array.Copy(data, 0, data_with_key, key.Length, data.Length);
            return data_with_key;
        }

    }
}