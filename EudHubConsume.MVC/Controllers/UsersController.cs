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
        Uri baseAddress = new Uri("https://localhost:44371/api");
        private readonly HttpClient _httpClient;

        public UsersController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;  
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           // var accessToken = HttpContext.Session.GetString("JWTtoken");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            List<UserViewModel> usersList = new List<UserViewModel>();
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress +
                "/users/GetAllUsers");

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(usersList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // var accessToken = HttpContext.Session.GetString("JWTtoken");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            UserViewModel usersList = new UserViewModel();
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress +
                 "/Users/GetUser?id=" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<UserViewModel>(data);
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
                    "/users/RegisterUser", content);

                if (response.StatusCode.ToString() == "BadRequest")
                {
                    string resData = await response.Content.ReadAsStringAsync();
                    TempData["errorMessage"] = resData;
                    return View("Create");
                }

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
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                //var accessToken = HttpContext.Session.GetString("JWTtoken");
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                UserViewModel user = new UserViewModel();

                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress +
                 "/Users/GetUser?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
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
        public async Task<IActionResult> Edit(int id,UpdateUserViewModel userModel)
        {
            
            try
           {
                //var accessToken = HttpContext.Session.GetString("JWTtoken");
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string data = JsonConvert.SerializeObject(userModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/users/UpdateUser?id=" + id, content);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //var accessToken = HttpContext.Session.GetString("JWTtoken");
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                UserViewModel user = new UserViewModel();

                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress +
                "/Users/GetUser?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    
                    string data = await response.Content.ReadAsStringAsync();
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
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
               
                HttpResponseMessage response = await _httpClient.DeleteAsync(_httpClient.BaseAddress +
                 "/Users/DeleteUser/" + id);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "User Deleted successfully!";
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

