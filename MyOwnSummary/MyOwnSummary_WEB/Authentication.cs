using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyOwnSummary_WEB.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MyOwnSummary_WEB
{
    public class Authentication
    {
        string Token { get; set; }

        public static async Task Authenticate()
        {
            string apiUrl = "https://localhost:7066/api/Authentication";

            CreateUser user = new CreateUser { UserName = "admin", Password = "admin" };

            string requestBody = JsonConvert.SerializeObject(user);

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(apiResponse);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }
}
