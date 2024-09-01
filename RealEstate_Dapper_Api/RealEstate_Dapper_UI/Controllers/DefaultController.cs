using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Models;
using Microsoft.Extensions.Options;

public class DefaultController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiSettings _settings;

    public DefaultController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _settings = settings.Value;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync(_settings.BaseUrl + "categories");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public async Task<PartialViewResult> PartialSearch()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:44364/api/Categories");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            return PartialView(values);
        }
        return PartialView();
    }
    [HttpPost]
    public IActionResult PartialSearch(string searchKeyValue, int propertyCategoryID, string city) 
    {
        TempData["searchKeyValue"] = searchKeyValue;
        TempData["propertyCategoryID"] = propertyCategoryID;
        TempData["city"] = city;

        return RedirectToAction("PropertyListWithSearch","Property");
    }       
}

