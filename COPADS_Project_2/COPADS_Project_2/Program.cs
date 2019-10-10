using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace COPADS_Project_2
{
    public class Program
    {
        static object locker = new object();
        public static BigInteger GeneratePrimeNumber(int bitsArgument)
        {
            RNGCryptoServiceProvider randomNumbers = new RNGCryptoServiceProvider();

            byte[] byteArray = new Byte[bitsArgument / 8];

            //Setter function
            randomNumbers.GetNonZeroBytes(byteArray);

            //Set the constructor of the bigInteger, return bigInt
            BigInteger bigInt = new BigInteger(byteArray);

            return bigInt;

        }
        public static bool checkPrimeNumber(BigInteger bigInt)
        {
            var checkPrime = new Extension();
            var isPrime = checkPrime.IsProbablyPrime(bigInt);
            return isPrime;
        }

        static void serialPrimeNumber(int bitsArgument, int tracker, int countsArgument)
        {
            //loop until your find 'count' many prime numbers
            while (tracker != countsArgument)
            {
                var primeNumber = GeneratePrimeNumber(bitsArgument);
                var prime = checkPrimeNumber(primeNumber);

                //If number is prime then your should print.
                if (prime)
                {
                    tracker += 1;
                    Console.WriteLine(primeNumber);
                }
            }
        }
        static void parallelPrimeFunction(int bitsArgument, int countsArgument)
        {
            var tracker = 0;
            Parallel.For(0, countsArgument, i =>
            {

                while (tracker != countsArgument)
                {
                    var primeNumber = GeneratePrimeNumber(bitsArgument);
                    var prime = checkPrimeNumber(primeNumber);

                    //If number is prime then your should print.
                    if (prime)
                    {
                        tracker += 1;
                        Console.WriteLine(primeNumber);
                    }
                }

            });
        }

        static void Main(string[] args)
        {

            //This will make sure to check for input less than 2 and more than 2
            if (args.Length < 1 || args.Length > 2)
            {
                Console.Error.WriteLine("dotnet run PrimeGen <bits> <count = 1>");
                Console.Error.WriteLine("- bits - the number of bits of the prime number, this must be a");
                Console.Error.WriteLine("\tmultiple of 8, and at least 32 bits.");
                Console.Error.WriteLine("- count - the number of prime numbers to generate, defaults to 1>");
            }
            else
            {
                //Process the argument with exactly 1 input argument
                if (args.Length == 1)
                {
                    Console.WriteLine("Using default count argument (count == 1)");

                    var bitsArgument = Convert.ToInt32(args[0].ToString());
                    var countsArgument = 1;
                    var tracker = 0;

                    //The number is multiple of 8
                    if (bitsArgument % 8 == 0 && bitsArgument >= 32)
                    {
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        serialPrimeNumber(bitsArgument, tracker, countsArgument);
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                            ts.Hours, ts.Minutes, ts.Seconds,
                                            ts.Milliseconds / 10);
                        Console.WriteLine("Time to Generate: " + elapsedTime);

                    }
                    else
                    {
                        Console.Error.WriteLine("The number is needs to be at least 32 bits and multiple of 8");
                    }
                }

                //Make sure to process the two arguments
                if (args.Length == 2)
                {
                    //check if first parameter is valid
                    var bitsArgument = Convert.ToInt32(args[0].ToString());
                    var countsArgument = Convert.ToInt32(args[1].ToString());


                    //The number is multiple of 8
                    if (bitsArgument % 8 == 0 && bitsArgument >= 32)
                    {

                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        parallelPrimeFunction(bitsArgument, countsArgument);
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;

                        // Format and display the TimeSpan value.
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                        Console.WriteLine("Time to Generate: " + elapsedTime);
                    }
                    else
                    {
                        Console.Error.WriteLine("The number is needs to be at least 32 bits and multiple of 8");
                    }
                }

            }
        }
    }
}
