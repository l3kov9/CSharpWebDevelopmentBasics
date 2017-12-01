using System;
using System.Net;

namespace _02.ValidateURL
{
    public class Program
    {
        public static void Main()
        {
            var url = Console.ReadLine();

            var webUtility = WebUtility.UrlDecode(url);

            var parsedUrl = new Uri(webUtility);

            Console.WriteLine(parsedUrl.Scheme);
            Console.WriteLine(parsedUrl.Host);
            Console.WriteLine(parsedUrl.Port);
            Console.WriteLine(parsedUrl.AbsolutePath);
            Console.WriteLine(parsedUrl.Query);
            Console.WriteLine(parsedUrl.Fragment);
        }
    }
}
