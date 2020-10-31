using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string pageUrl = args[0];

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(pageUrl);

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z0-9]+@[a-z.]+");

                var matches = regex.Matches(html);
                foreach(var match in matches)
                {
                    Console.WriteLine(match);
                }
            }

            Console.WriteLine("Koniec!");
        }
    }
}
