using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace WebApplication2.Data.Models
{
    public class Museum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Exhibition> Exhibitions { get; set; }
    }
}
