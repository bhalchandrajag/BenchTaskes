using EudHubConsume.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EudHubConsume.MVC.Controllers
{
    public class CoursesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44371/api");
        private readonly HttpClient _httpClient;

        public CoursesController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {

            List<CourseViewModel> coursesList = new List<CourseViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                "/courses/GetAllCourses").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                coursesList = JsonConvert.DeserializeObject<List<CourseViewModel>>(data);
            }
            return View(coursesList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseViewModel courseModel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(courseModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress +
                    "/courses/RegisterCourse", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Course created successfully!";
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
        public IActionResult GetResult(int id)
        {
            try
            {
                List<CourseViewModel> user = new List<CourseViewModel>();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                    "/courses/Getcourseaddedbyedu?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<List<CourseViewModel>>(data);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                CourseViewModel courseView = new CourseViewModel();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                    "/courses/GetCourseDetails?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data =  response.Content.ReadAsStringAsync().Result;
                    courseView = JsonConvert.DeserializeObject<CourseViewModel>(data);
                }
                return View(courseView);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseViewModel courseModel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(courseModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress +
                    "/courses/UpdateCourse", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Course updated successfully!";
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
