namespace RealEstate_Dapper_Api.Dtos.ProductDtos
{
    public class GetProductByProductIdDto
    {
        public int productID { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public int ProductCategory { get; set; }
        public string CategoryName { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string CoverImage { get; set; }
        public bool DealOfTheDay { get; set; }
        public DateTime AdvertisementDate { get; set; }
        public string Destcription { get; set; }
        public string SlugUrl { get; set; }


    }
}
