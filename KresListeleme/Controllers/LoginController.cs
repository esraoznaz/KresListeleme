using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using KresListeleme.Models;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KresListeleme.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            var client = new HttpClient(handler);
            var loginData = new { YetkiliEMail = email, YetkiliSifre = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7006/api/Token", content);

            if (response.IsSuccessStatusCode)
            {
                string tokenYazi = await response.Content.ReadAsStringAsync();
                var tokenResponse1 = new TokenResponse
                {
                    Token = tokenYazi // jsonResponse doğrudan token içeriyorsa
                };

                // Artık tokenResponse1.Token içindeki token'ı kullanabilirsiniz
                string token = tokenResponse1.Token;
                //var json = JsonConvert.SerializeObject(tokenResponse);
                // string token = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse)?.Token;


                if (token != null)
                {
                   HttpContext.Session.SetString("Token", token);
                   return RedirectToAction("ListYetkili", "Home");
                }
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Error: {response.StatusCode}, {errorResponse}";
            }

            ViewBag.Error = "Geçersiz Giriş.";
            return View();
        }
    }
    
}
