using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using MyOwnSummary_WEB.Models;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.Metadata;
using System.Text;

namespace MyOwnSummary_WEB.Controllers
{
    public class AutheticationController : Controller
    {
        public async Task<IActionResult> Login()
        {
            if (Request.Method == "POST")
            {
                string username = Request.Form["Username"];
                string password = Request.Form["Password"];
                CreateUser userApi = new CreateUser{ UserName = username, Password = password };
                string urlApi = "https://localhost:7066/api/Authentication";
                ApiResponse? apiResponse = new();
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        string jsonUser = JsonConvert.SerializeObject(userApi);
                        StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await httpClient.PostAsync(urlApi, content);
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                            if(apiResponse != null && apiResponse.StatusCode == HttpStatusCode.OK && apiResponse.Result != null)
                            {                                
                                User user = new User { UserName = userApi.UserName, Password = userApi.Password, Token = apiResponse.Result.ToString() };
                                HttpContext.Session.Set("AuthenticatedUser", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user)));
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                Console.WriteLine($"Error al obtener el token");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Excepción: {ex.Message}");
                    }
                }
            
            }
            return View();
        }
    }
}
