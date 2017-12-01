using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCalculating
{
    public class Startup
    {
        private static string result;

        public static void Main()
        {
            Console.WriteLine("Calculating ...");
            Task.Run(()=> CalculateSlowly());

            Console.WriteLine("Enter command:");

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "show")
                {
                    if (result == null)
                    {
                        Console.WriteLine("Still calculating ... Please wait!");
                    }
                    else
                    {
                        Console.WriteLine($"Result is: {result}");
                    }
                }
                if (line == "exit")
                {
                    break;
                }
            }
        }

        private static void CalculateSlowly()
        {
            Thread.Sleep(5000);
            result = "42";
        }
    }
}
