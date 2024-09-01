using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.PopularLocationDtos;

namespace RealEstate_Dapper_UI.ViewComponents.HomePage
{
    public class _DefaultProductListExploreCitiesComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultProductListExploreCitiesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:44364/api/PopularLocation");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsonData =await responsemessage.Content.ReadAsStringAsync();
                var value=JsonConvert.DeserializeObject<List<ResultPopularLocationDto>>(jsonData);
                return View(value);
            }

            return View();
        }
    }
}
