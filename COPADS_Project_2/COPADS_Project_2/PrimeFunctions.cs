//@author: Isaias Villalobos
//@date: 10/10/2019
//@version: 1.1 --implemented the Object Oriented style into this project.
//Description:    This class is going to have many functions to help with generating prime numbers, 
//                checking if a prime number is really prime, function to help with parallel compute of prime numbers

using COPADS_Project_2;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class PrimeFunction
{
    private readonly object locker = new object();

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
            var isPrime = bigInt.IsProbablyPrime();
            return isPrime;
        }

        public void parallelPrimeFunction(int bitSize, int countsArgument)
        {
            List<BigInteger> intList = new List<BigInteger>();
            int numPrimeSeen = 0;

            Parallel.For(0, countsArgument, i =>
            {
              
                var primeNumber = GeneratePrimeNumber(bitSize);
                var isPrime = checkPrimeNumber(primeNumber);
                lock (locker)
                {
                    if (isPrime)
                    {
                        numPrimeSeen += 1;
                        intList.Add(primeNumber);
                  

                    }
                }
                

            });

            for(int i = 0; i < intList.Count; i++)
            {
                Console.Write(i + ": " + intList[i]);
            }

    }
}
