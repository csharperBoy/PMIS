using PMIS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Bases
{
    internal static class Hasher
    {
        public class HasherHMACSHA512 : IHasher
        {
            private static string hash;
            void IHasher.Hash(string text)
            {
                Byte[] B1 = UTF8Encoding.UTF8.GetBytes(text);
                Byte[] B2 = UTF8Encoding.UTF8.GetBytes("HAMA" + text);
                Byte[] B3 = HMACSHA512.HashData(B1, B2);

                hash = BitConverter.ToString(B2).Replace("-", "");
            }
            public static string Hash(string text)
            {
                ((IHasher)new HasherHMACSHA512()).Hash(text);
                return hash;
            }
        }
    }
}
