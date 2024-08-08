using EudHubConsume.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EudHubConsume.MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> LoginUser(LoginModel loginModel)
        {

            using(var httpClient=new HttpClient())
            {
                StringContent stringContent=new StringContent(JsonConvert.SerializeObject(loginModel),Encoding.UTF8
                    ,"application/json");
                using (var response= await httpClient.PostAsync("https://localhost:44371/api/Authentication/UserLogin", stringContent))
                {
                    string token= await response.Content.ReadAsStringAsync();
                    if (token == "Username and Password is not correct.")
                    {
                        ViewBag.Message = "Incorrect Password or Email";
                        return Redirect("~/Login/Index");
                    }
                    HttpContext.Session.SetString("JwTtoken", token);
                }
                return Redirect("~/Dashboard/Index");
            }

        }

        public IActionResult SignOut()
        {
           HttpContext.Session.Clear();
            return Redirect("~/Login/Index");
        }
    }
}
