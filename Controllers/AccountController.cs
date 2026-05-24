using AWCDigitalCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace AWCDigitalCommerce.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Login()
        {
            _logger.LogDebug("Loading AcccountController:Login");

            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User model)
        {
            _logger.LogDebug("AcccountController:Validating Login data");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = _configuration["ApiSettings:BaseUrl"]!;

                client.BaseAddress = new Uri(apiUrl);

                LoginRequest data = new LoginRequest
                {
                    UserPIN = model.UserPIN,
                    UserPW = model.UserPW
                };

                var json = JsonConvert.SerializeObject(data);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultado = await response.Content.ReadAsStringAsync();

                    // LOGIN OK
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Usuario o PIN incorrecto";
                    return View(model);
                }
            }
        }
    }
}
