using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvenNumbersThread
{
    public class Program
    {
        public static void Main()
        {
            var min = int.Parse(Console.ReadLine());
            var max = int.Parse(Console.ReadLine());
            var thread = new Thread(() =>
              {
                  PrintEvenNumbes(min, max);
              });

            thread.Start();
            thread.Join();
        }

        private static void PrintEvenNumbes(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
