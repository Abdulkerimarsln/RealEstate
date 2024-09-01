using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_UI.Dtos.LoginDtos;
using RealEstate_Dapper_UI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RealEstate_Dapper_UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index (CreateLoginDto loginDto)
        {
            var client=_httpClientFactory.CreateClient();
            var content=new StringContent(JsonSerializer.Serialize(loginDto),Encoding.UTF8,"application/json");
            var response=await client.PostAsync("https://localhost:44364/api/Login", content);
            if (response.IsSuccessStatusCode) { 
                var JsonData =await response.Content.ReadAsStringAsync();
                var TokenModel = JsonSerializer.Deserialize<JwsResponseModel>(JsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                if (TokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token=handler.ReadJwtToken(TokenModel.Token);
                    var claims = token.Claims.ToList();
                    if (TokenModel.Token != null)
                    {
                        claims.Add(new Claim("realestatetoken", TokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = TokenModel.ExpireDate,
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProps);
                        return RedirectToAction("Index", "Employee");
                    }
                }
            }
                return View();


        }
    }
}
