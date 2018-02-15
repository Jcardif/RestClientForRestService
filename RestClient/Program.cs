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
                HttpResponseMessage response = await client.GetAsync("api/Person/4");
                if (response.IsSuccessStatusCode)
                {
                    string result=await response.Content.ReadAsStringAsync();
                    Person person = new Person();
                    //person = JsonConvert.DeserializeObject<Person>(result);
                    Console.WriteLine(result);
                }

                response = await client.GetAsync("api/Person");
                if (response.IsSuccessStatusCode)
                {
                    string personLit = await response.Content.ReadAsStringAsync();
                    List<Person> personList = JsonConvert.DeserializeObject<List<Person>>(personLit);
                   // Console.WriteLine("ID\tFirst NamE\tLast Name\tStart Date\tEnd Date");
                    for (int i = 0; i < personList.Count; i++)
                    {
                        Console.WriteLine($"{personList[i].ID}\t{personList[i].FirstName}\t{personList[i].LastName}\t{personList[i].StartDate.ToLongDateString()}\t{personList[i].EndDate.ToLongDateString()}");
                    }
                }
            }
        }
    }
}
