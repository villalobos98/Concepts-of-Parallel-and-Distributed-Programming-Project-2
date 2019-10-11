//@author: Isaias Villalobos
//@date: 10/10/2019
//@version: 1.1 --implemented the Object Oriented style into this project.
//Description:    This class is going to have many functions to help with generating prime numbers, 
//                checking if a prime number is really prime, function to help with parallel compute of prime numbers

using COPADS_Project_2;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class PrimeFunction
{
        
        public BigInteger GeneratePrimeNumber(int bitSize)
        {
            RNGCryptoServiceProvider randomNumbers = new RNGCryptoServiceProvider();
            byte[] byteArray = new Byte[bitSize / 8];

            randomNumbers.GetNonZeroBytes(byteArray);

            BigInteger bigInt = new BigInteger(byteArray);
            
            return bigInt;
        }

        public bool checkPrimeNumber(BigInteger bigInt)
        {
            var checkPrime = new Extension();
            var isPrime = checkPrime.IsProbablyPrime(bigInt);
            return isPrime;
        }

        public void serialPrimeNumber(int bitSize, int countsArgument)
        {
            var tracker = 0;
            //loop until your find 'count' many prime numbers
            while (tracker != countsArgument)
            {
                var primeNumber = GeneratePrimeNumber(bitSize);
                var prime = checkPrimeNumber(primeNumber);

                //If number is prime then your should print.
                if (prime)
                {
                    tracker += 1;
                    Console.WriteLine(primeNumber);
                }
            }
        }

        public void parallelPrimeFunction(int bitSize, int countsArgument)
        {
            var tracker = 0;
            Parallel.For(0, countsArgument, i =>
            {
                while (tracker != countsArgument)
                {
                    var primeNumber = GeneratePrimeNumber(bitSize);
                    var isPrime = checkPrimeNumber(primeNumber);

                    //If number is prime then your should print.
                    if (isPrime)
                    {
                        tracker += 1;
                        Console.Write(tracker.ToString() + ": ");
                        Console.WriteLine(primeNumber);
                        Console.WriteLine();
                    }
                }
            });
        }
}
