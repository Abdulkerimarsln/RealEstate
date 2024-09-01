namespace RealEstate_Dapper_Api.Dtos.ProductDtos
{
    public class ResultLast3ProductWithCategoryDto
    {
        public int productID { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public int ProductCategory { get; set; }
        public string CoverImage { get; set; }
        public string CategoryName { get; set; }
        public string Destcription { get; set; }
        public DateTime AdvertisementDate { get; set; }
    }
}
