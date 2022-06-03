using WebApplication2.Data.Models;

namespace WebApplication2.Data.Dtos
{
    public class OrderDto
    {
        public OrderDto(Order order)
        {
            Id = order.Id;
            UserId = order.User.Id;
            ExhibitionId = order.Exhibition.Id;
            Cost = order.Cost;
            CreationTimestamp = order.CreationTimestamp;
            Discount = order.Discount;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExhibitionId { get; set; }
        public double Cost { get; set; }
        public double Discount { get; set; }
        public long CreationTimestamp { get; set; }
    }
}
