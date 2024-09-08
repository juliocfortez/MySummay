using Microsoft.AspNetCore.Mvc;
using MyOwnSummary_WEB.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MyOwnSummary_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public User? AuthenticatedUser { get; private set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
            AuthenticatedUser = authenticatedUserBytes != null
                ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                : null;

            if (AuthenticatedUser == null)
            {
                return RedirectToAction("Authetication", "Login");
            }
            return View(AuthenticatedUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Notes()
        {
            string urlApi = "https://localhost:7066/api/Note/DataForViewNotes";
            var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
            AuthenticatedUser = authenticatedUserBytes != null
                ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                : null;

            if (AuthenticatedUser == null)
            {
                return RedirectToAction("Authetication", "Login");
            }
            string? token = AuthenticatedUser.Token;
            ApiResponse? apiResponse = new();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlApi);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                        if (apiResponse != null && apiResponse.StatusCode == HttpStatusCode.OK && apiResponse.Result != null)
                        {
                            NoteViewDto? notes = JsonConvert.DeserializeObject<NoteViewDto>(JsonConvert.SerializeObject(apiResponse.Result));
                            return View(notes);
                        }
                        else
                        {
                            Console.WriteLine($"Error al obtener las notas");
                            return View();
                        }
                    }
                    else if(response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Authetication", "Login");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch
                {
                    return View("Error");
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}