namespace WebApplication2.Data.Dtos;

public class OrderCreationDto
{
    public int UserId { get; set; }
    public int ExhibitionId { get; set; }
    public double Cost { get; set; }
    public double Discount { get; set; }
}
