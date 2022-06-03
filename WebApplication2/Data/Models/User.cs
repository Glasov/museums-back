using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(Order = 2)]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public long RegistrationTimestamp { get; set; }
        public long LastEditTimestamp { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
