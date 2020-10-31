using System;
using System.Collections.Generic;
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
            if (args.Length == 0)
            {
                throw new ArgumentNullException();
            }

            string pageUrl = args[0];
            var urlRegex = new Regex("http[s]?://[a-zA-Z.]+");
            bool isProperUrlString = urlRegex.IsMatch(pageUrl);

            if (!isProperUrlString)
            {
                throw new ArgumentException();
            }

            var httpClient = new HttpClient();

            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync(pageUrl);
                httpClient.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    var html = await response.Content.ReadAsStringAsync();
                    var emailRegex = new Regex("[a-z0-9]+@[a-z.]+");

                    var matches = emailRegex.Matches(html);
                    

                    if (matches.Count == 0)
                    {
                        Console.WriteLine("Nie znaleziono adresów email.");
                    } else
                    {
                        HashSet<string> alreadyPrinted = new HashSet<string>();
                        foreach (var match in matches)
                        {
                            var email = match.ToString();
                            if (!alreadyPrinted.Contains(email)) 
                            {
                                Console.WriteLine(email);
                                alreadyPrinted.Add(email);
                            }
                            
                        }
                    }
                }
            } catch
            {
                Console.WriteLine("Błąd podczas pobierania strony");
            }
            
            Console.WriteLine("Koniec!");
        }
    }
}
