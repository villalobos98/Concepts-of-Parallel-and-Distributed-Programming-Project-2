//@author: Isaias Villalobos
//@date: 10/10/2019
//@version: A simple extension class. Will allow use of extension method.
//Description: This class contains one class which will a thorough check of a value that is a BigInteger.
//             The purpose of the BigInteger class is that, generating prime numbers that are very large,
//             can be handled by this class. Regular types will not be helpful. The isProbablyPrime class, 
//             can check if a value, aka prime number is indeed a prime number.

using System;
using System.Numerics;

namespace COPADS_Project_2
{

    public static class Extension
    {

        public static bool IsProbablyPrime(this BigInteger value, int witnesses = 10)
        {
            if (value <= 1) return false;
            if (witnesses <= 0) witnesses = 10;
            BigInteger d = value - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }
            Byte[] bytes = new Byte[value.ToByteArray().LongLength];
            BigInteger a;
            for (int i = 0; i < witnesses; i++)
            {
                do
                {
                    var Gen = new Random();
                    Gen.NextBytes(bytes);
                    a = new BigInteger(bytes);
                } while (a < 2 || a >= value - 2);
                BigInteger x = BigInteger.ModPow(a, d, value);
                if (x == 1 || x == value - 1) continue;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, value);
                    if (x == 1) return false;
                    if (x == value - 1) break;
                }
                if (x != value - 1) return false;
            }
            return true;
        }
    }
}