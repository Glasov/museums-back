using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Data.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        public User User { get; set; }
        public Exhibition Exhibition { get; set; }
        public double Cost { get; set; }
        public double Discount { get; set; }
        public long CreationTimestamp { get; set; }
    }
}
