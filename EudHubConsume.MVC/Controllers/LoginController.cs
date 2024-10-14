using EudHubConsume.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System.Text;
using System.Text.Json;

namespace EudHubConsume.MVC.Controllers
{
    public class LoginController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44371/api");
        private readonly HttpClient _httpClient;

        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> LoginUser(LoginModel loginModel)
        {

            try
            {
               // UserDetailsVM userModel = new UserDetailsVM();

                string data = JsonConvert.SerializeObject(loginModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress +
                    "/Authentication/UserLogin", content);

                if (response.StatusCode.ToString() == "BadRequest")
                {
                    string message = await response.Content.ReadAsStringAsync();
                    TempData["errorMessage"] = message;
                    return View("Index");
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    string message = await response.Content.ReadAsStringAsync();
                    TempData["errorMessage"] = message;
                    return View("Index");
                }
                else if (response.IsSuccessStatusCode)
                {

                    var token = await response.Content.ReadAsStringAsync();

                    var userprofile = JsonConvert.DeserializeObject<UserDetailsVM>(token);

                    var userSessionModel = new UserDetailsVM
                    {
                        UserId = userprofile.UserId,
                        FirstName = userprofile.FirstName,
                        LastName = userprofile.LastName
                    };

                    HttpContext.Session.SetString("UserDetailsVM", JsonConvert.SerializeObject(userSessionModel));
                    //TempData["UserData"] =  JsonConvert.SerializeObject(userprofile);

                    //userModel.FirstName = userModel.FirstName;

                    //TempData["UserData"] = userModel.FirstName;
                   // ViewBag.MyData = userModel;

                    return RedirectToAction("Index","Dashboard");
                }
               
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("Index");
            }
            return View("Index");

        }




        public IActionResult SignOut()
        {
           HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
