using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }
        static async Task RunAsync()
        {
            using(var client =new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49702/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("GET");
                HttpResponseMessage response = await client.GetAsync("api/Person/6");
                if (response.IsSuccessStatusCode)
                {
                    string result=await response.Content.ReadAsStringAsync();
                    Person person = new Person();
                    //person = JsonConvert.DeserializeObject<Person>(result);
                    Console.WriteLine(result);
                }
            }
        }
    }
}
