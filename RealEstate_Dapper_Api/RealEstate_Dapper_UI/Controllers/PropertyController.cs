using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDetailDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44364/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> PropertyListWithSearch(string searchKeyValue, int propertyCategoryID, string city)
        {
            ViewBag.v = TempData["searchKeyValue"];
            ViewBag.y = TempData["propertyCategoryID"];
            ViewBag.z = TempData["city"];
            searchKeyValue = TempData["searchKeyValue"].ToString();
            propertyCategoryID = int.Parse(TempData["propertyCategoryID"].ToString());
            city = TempData["city"].ToString();
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44364/api/Products/ResultProductWithSearchList?searchKeyValue={searchKeyValue}&propertyCategoryID={propertyCategoryID}&city={city}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithSearchListDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet("Property/{slug}/{id}")]
        public async Task<IActionResult> PropertySingle(string slug,int id)
        {
            ViewBag.i = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44364/api/Products/GetProductByProductId?id=" + id);
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);

            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:44364/api/PRoductDetails/GetProductDetailByID?id=" + id);
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<GetProductDetailByIDDto>(jsonData1);

            ViewBag.ProductID=values.ProductID;
            ViewBag.title1= values.Title;
            ViewBag.price= values.Price;
            ViewBag.city= values.City;
            ViewBag.district= values.District;
            ViewBag.address= values.Address;
            ViewBag.type= values.Type;
            ViewBag.slugUrl = values.SlugUrl;


            ViewBag.bathCount = values1.BathCount;
            ViewBag.bedCount= values1.BedRoomCount;
            ViewBag.size=values1.ProductSize;
            ViewBag.description=values.Destcription;
            ViewBag.roomCount=values1.RoomCount;
            ViewBag.garagesize=values1.GarageSize;
            ViewBag.buildyear = values1.BuildYear;
            ViewBag.location= values1.Location;
            ViewBag.videourl= values1.VideUrl;



            var timeSpan = DateTime.Now - values.AdvertisementDate;

            string timeAgo;

            if (timeSpan.TotalSeconds < 60)
                timeAgo = $"{timeSpan.Seconds} saniye önce";
            else if (timeSpan.TotalMinutes < 60)
                timeAgo = $"{timeSpan.Minutes} dakika önce";
            else if (timeSpan.TotalHours < 24)
                timeAgo = $"{timeSpan.Hours} saat önce";
            else if (timeSpan.TotalDays < 30)
                timeAgo = $"{timeSpan.Days} gün önce";
            else if (timeSpan.TotalDays < 365)
                timeAgo = $"{timeSpan.Days / 30} ay önce";
            else
                timeAgo = $"{timeSpan.Days / 365} yıl önce";

            ViewBag.Datetime = timeAgo;

            string slugFromTitle=CreateSlug(values.Title);
            ViewBag.slugUrl=slugFromTitle;

            return View();
        }
        private string CreateSlug(string title)
        {
            title = title.ToLowerInvariant(); // Küçük harfe çevir
            title = title.Replace(" ", "-"); // Boşlukları tire ile değiştir
            title = System.Text.RegularExpressions.Regex.Replace(title, @"[^a-z0-9\s-]", ""); // Geçersiz karakterleri kaldır
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s+", " ").Trim(); // Birden fazla boşluğu tek boşluğa indir ve kenar boşluklarını kaldır
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s", "-"); // Boşlukları tire ile değiştir

            return title;
        }
    }
}
