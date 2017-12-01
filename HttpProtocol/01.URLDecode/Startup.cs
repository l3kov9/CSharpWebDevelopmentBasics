using System;
using System.Net;

namespace _01.URLDecode
{
    public class Startup
    {
        public static void Main()
        {
            while (true)
            {
                var url = Console.ReadLine();
                if (url == "END")
                {
                    break;
                }

                var webUtility = WebUtility.UrlDecode(url);

                Console.WriteLine(webUtility);
            }
        }
    }
}
