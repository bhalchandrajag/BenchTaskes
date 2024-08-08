using EudHubConsume.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace EudHubConsume.MVC.Controllers
{
    public class UsersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44371/api/");
        private readonly HttpClient _httpClient;

        public UsersController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;  
        }

        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            //var accessToken=HttpContext.Session.GetString("JwTtoken");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
            List<UserViewModel> usersList = new List<UserViewModel>();
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress +
                "users/GetAllUsers");

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(usersList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userModel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(userModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress +
                    "users/RegisterUser", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "User created successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                UserViewModel user = new UserViewModel();

                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                 "Users/GetUser?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserViewModel>(data);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userModel)
        {
            
            try
            {
               var _httpClient1 = new HttpClient();
                _httpClient1.BaseAddress = new Uri("https://localhost:44371/api/");
                string data = JsonConvert.SerializeObject(userModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpClient1.PutAsJsonAsync("Users/UpdateUser", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "User details updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

    }
}

