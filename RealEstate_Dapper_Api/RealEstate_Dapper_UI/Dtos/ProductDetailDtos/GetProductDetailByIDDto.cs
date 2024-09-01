namespace RealEstate_Dapper_UI.Dtos.ProductDetailDtos
{
    public class GetProductDetailByIDDto
    {
        public int ProductDetailID { get; set; }
        public int BedRoomCount { get; set; }
        public int ProductSize { get; set; }
        public int BathCount { get; set; }
        public int RoomCount { get; set; }
        public int GarageSize { get; set; }
        public int BuildYear { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string VideUrl { get; set; }
        public int ProductId { get; set; }
    }
}
