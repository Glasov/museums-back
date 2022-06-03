using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Data.Models
{
    public class Exhibition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        public Museum Museum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long StartTimestamp { get; set; }
        public long EndTimestamp { get; set; }
        public double Cost { get; set; }
        public IEnumerable<ExhibitionItem> ExhibitionItems { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
