//@author: Isaias Villalobos
//@date: 10/10/2019
//@version: 1.1 -- This class now uses the methods in the PrimeFunctions class.
//Description:     This is the class that will use methods in prime class, this class handles command line arguments
//                 and makes sure to print out to the console.

using System;
using System.Diagnostics;

namespace COPADS_Project_2
{
    public class Program
    {

        public  void CallSerialPrimeNumber(int bitsArgument,int countsArgument)
        {
            PrimeFunction functions = new PrimeFunction();
            functions.serialPrimeNumber(bitsArgument, countsArgument);
        }
        
        public  void CallParallelPrimeNumber(int bitsArgument, int countsArgument)
        {
            PrimeFunction functions = new PrimeFunction();
            functions.parallelPrimeFunction(bitsArgument, countsArgument);
        }

        public static void Main(string[] args)
        {

            //This will make sure to check for input less than 2 and more than 2
            if (args.Length < 1 || args.Length > 2)
            {
                Console.Error.WriteLine("dotnet run PrimeGen <bits> <count = 1>");
                Console.Error.WriteLine("    - bits - the number of bits of the prime number, this must be a");
                Console.Error.WriteLine("      multiple of 8, and at least 32 bits.");
                Console.Error.WriteLine("    - count - the number of prime numbers to generate, defaults to 1");
            }
            else
            {
                //Process the argument with exactly 1 input argument
                if (args.Length == 1)
                {

                    var bitsArgument = Convert.ToInt32(args[0].ToString());
                    var countsArgument = 1;

                    Console.Write("BitLength: " + bitsArgument.ToString() + " bits");

                    //The number is multiple of 8
                    if (bitsArgument % 8 == 0 && bitsArgument >= 32)
                    {

                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        Program program = new Program();
                        program.CallSerialPrimeNumber(bitsArgument, countsArgument);
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

                //Make sure to process the 2 arguments
                if (args.Length == 2)
                {
                    //check if first parameter is valid
                    var bitsArgument = Convert.ToInt32(args[0].ToString());
                    var countsArgument = Convert.ToInt32(args[1].ToString());

                    Console.Write("BitLength: " + bitsArgument.ToString() + " bits");

                    //The number is multiple of 8
                    if (bitsArgument % 8 == 0 && bitsArgument >= 32)
                    {

                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        Program program = new Program();
                        program.CallParallelPrimeNumber(bitsArgument, countsArgument);
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
            }
        }
    }
}
