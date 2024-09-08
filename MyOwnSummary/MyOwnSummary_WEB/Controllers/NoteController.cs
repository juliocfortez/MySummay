using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using MyOwnSummary_WEB.Models;

namespace MyOwnSummary_WEB.Controllers
{
    public class NoteController : Controller
    {
        public User? AuthenticatedUser { get; private set; }
        public async Task<IActionResult> Update(int noteId)
        {
            if(Request.Method != "GET") 
            {
                if (Request.Form["_method"] == "put")
                {
                    NoteDto note = new NoteDto
                    {
                        Id = Convert.ToInt32(Request.Form["Id"]),
                        CategoryId = Convert.ToInt32(Request.Form["Category"]),
                        LanguageId = Convert.ToInt32(Request.Form["Language"]),
                        Description = Request.Form["Description"],
                        SourceText = Request.Form["SourceText"],
                        Translate = Request.Form["Translate"],
                        Pronunciation = Request.Form["Pronunciation"],
                        UserId = Convert.ToInt32(Request.Form["User"])
                    };
                    string urlApi = "https://localhost:7066/api/Note/" + note.Id.ToString();
                    var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
                    AuthenticatedUser = authenticatedUserBytes != null
                        ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                        : null;

                    if (AuthenticatedUser == null)
                    {
                        return RedirectToAction("Login", "Authetication");
                    }
                    string? token = AuthenticatedUser.Token;
                    ApiResponse? apiResponse = new();
                    using (HttpClient httpClient = new HttpClient())
                    {
                        try
                        {
                            string jsonNote = JsonConvert.SerializeObject(note);
                            StringContent content = new StringContent(jsonNote, Encoding.UTF8, "application/json");
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            HttpResponseMessage response = await httpClient.PutAsync(urlApi, content);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Notes", "Home");
                            }
                            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                return RedirectToAction("Login", "Authetication");
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
                else
                {
                    return RedirectToAction("Notes", "Home");
                }
            }
            else
            {
                string urlApi = "https://localhost:7066/api/Note/" + noteId;
                var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
                AuthenticatedUser = authenticatedUserBytes != null
                    ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                    : null;

                if (AuthenticatedUser == null)
                {
                    return RedirectToAction("Login", "Authetication");
                }
                string? token = AuthenticatedUser.Token;
                ApiResponse? apiResponse = new();
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage response = await httpClient.GetAsync(urlApi);
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                            if (apiResponse != null && apiResponse.StatusCode == HttpStatusCode.OK && apiResponse.Result != null)
                            {
                                NoteDto? note = JsonConvert.DeserializeObject<NoteDto>(JsonConvert.SerializeObject(apiResponse.Result));
                                return View(note);
                            }
                            else
                            {
                                return RedirectToAction("Notes", "Home");
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return RedirectToAction("Login", "Authetication");
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
            
        }

        public async Task<IActionResult> Create()
        {
            if (Request.Method == "POST")
            {
                    CreateNoteDto note = new CreateNoteDto
                    {
                        CategoryId = Convert.ToInt32(Request.Form["Category"]),
                        LanguageId = Convert.ToInt32(Request.Form["Language"]),
                        Description = Request.Form["Description"],
                        SourceText = Request.Form["SourceText"],
                        Translate = Request.Form["Translate"],
                        Pronunciation = Request.Form["Pronunciation"],
                        UserId = Convert.ToInt32(Request.Form["User"])
                    };
                    string urlApi = "https://localhost:7066/api/Note";
                    var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
                    AuthenticatedUser = authenticatedUserBytes != null
                        ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                        : null;

                    if (AuthenticatedUser == null)
                    {
                        return RedirectToAction("Login", "Authetication");
                    }
                    string? token = AuthenticatedUser.Token;
                    ApiResponse? apiResponse = new();
                    using (HttpClient httpClient = new HttpClient())
                    {
                        try
                        {
                            string jsonNote = JsonConvert.SerializeObject(note);
                            StringContent content = new StringContent(jsonNote, Encoding.UTF8, "application/json");
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            HttpResponseMessage response = await httpClient.PostAsync(urlApi, content);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Notes", "Home");
                            }
                            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                return RedirectToAction("Login", "Authetication");
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
            else
            {
                string urlApi = "https://localhost:7066/api/Note/DataForViewCreateNote";
                var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
                AuthenticatedUser = authenticatedUserBytes != null
                    ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                    : null;

                if (AuthenticatedUser == null)
                {
                    return RedirectToAction("Login", "Authetication");
                }
                string? token = AuthenticatedUser.Token;
                ApiResponse? apiResponse = new();
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage response = await httpClient.GetAsync(urlApi);
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                            if (apiResponse != null && apiResponse.StatusCode == HttpStatusCode.OK && apiResponse.Result != null)
                            {
                                DataForCreateNote? data = JsonConvert.DeserializeObject<DataForCreateNote>(JsonConvert.SerializeObject(apiResponse.Result));
                                return View(data);
                            }
                            else
                            {
                                return RedirectToAction("Notes", "Home");
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return RedirectToAction("Login", "Authetication");
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
        }

        public async Task<HttpStatusCode> Delete(int noteId)
        {
            string urlApi = "https://localhost:7066/api/Note/"+noteId;
            var authenticatedUserBytes = HttpContext.Session.Get("AuthenticatedUser");
            AuthenticatedUser = authenticatedUserBytes != null
                ? JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(authenticatedUserBytes))
                : null;

            if (AuthenticatedUser == null)
            {
                return HttpStatusCode.Unauthorized;
            }
            string? token = AuthenticatedUser.Token;
            ApiResponse? apiResponse = new();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await httpClient.DeleteAsync(urlApi);
                    if (response.IsSuccessStatusCode)
                    {
                        return HttpStatusCode.OK;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }
                }
                catch
                {
                    return HttpStatusCode.InternalServerError;
                }
            }
        }
    }
}
