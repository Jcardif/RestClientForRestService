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
                HttpResponseMessage response;
                Console.WriteLine("GET");
                int id = Convert.ToInt32(Console.ReadLine());
                response = await client.GetAsync($"api/Person/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string result=await response.Content.ReadAsStringAsync();
                    Person person = new Person();
                    //person = JsonConvert.DeserializeObject<Person>(result);
                    Console.WriteLine(result);
                }

                Console.WriteLine("Get all");
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

                Console.WriteLine("Post");
                Person p = new Person()
                {
                    ID = Convert.ToInt32(Console.ReadLine()),
                    FirstName = Console.ReadLine(),
                    LastName = Console.ReadLine(),
                    PayRate = Convert.ToDouble(Console.ReadLine()),
                    StartDate = Convert.ToDateTime(Console.ReadLine()),
                    EndDate = Convert.ToDateTime(Console.ReadLine())
                }; 
          
                 response = await client.PostAsJsonAsync("api/Person/", p);
                if (response.IsSuccessStatusCode)
                {
                    Uri personUri = response.Headers.Location;
                    Console.WriteLine(personUri);
                }
            }
        }
    }
}
