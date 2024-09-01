namespace RealEstate_Dapper_Api.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int ProductCategory { get; set; }
        public string CoverImage { get; set; }
        public string Address { get; set; }
        public string Destcription { get; set; }
        public string Type { get; set; }
        public bool DealOfTheDay { get; set; }
        public DateTime AdvertisementDate { get; set; }
        public bool ProductStatus { get; set; }
        public int EmployeeID { get; set; }

    }
}
