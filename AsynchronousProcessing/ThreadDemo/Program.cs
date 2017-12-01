using System;
using System.Threading;

namespace ThreadDemo
{
    public class Program
    {
        public static void Main()
        {
            var thread = new Thread(() =>
              {
                  PrintNumbersSlowly();
              });

            thread.Start();

            Console.WriteLine("Printing ... ");

            Thread.Sleep(5000);
            Console.WriteLine("Still printing ...");
            thread.Join();

            Console.WriteLine("Print finished");
        }

        private static void PrintNumbersSlowly()
        {
            for (int i = 0; i < 7; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
            }
        }
    }
}
