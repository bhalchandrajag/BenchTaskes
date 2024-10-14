using EudHubConsume.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EudHubConsume.MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            //if (TempData["UserData"] is string userprofileJson)
            //{
            //    var userdata = JsonConvert.DeserializeObject<UserDetailsVM>(userprofileJson);
            //    return View(userdata);
            //}
            var obj = HttpContext.Session.GetString("UserDetailsVM");
            var myobj= JsonConvert.DeserializeObject<UserDetailsVM>(obj);

            return View(myobj);
            // return RedirectToAction("Index","Login");

        } 
    }
}
