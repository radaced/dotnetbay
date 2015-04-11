using System;
using System.Data.Entity.SqlServer;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using DotNetBay.WebApi.Controllers;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var typesLoaded = new[] { typeof(StatusController), typeof(SqlProviderServices) };

            var baseAddress = "http://localhost:9001/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/status").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();
        }
    }
}
