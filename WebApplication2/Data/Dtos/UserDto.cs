using WebApplication2.Data.Models;

namespace WebApplication2.Data.Dtos
{
    public class UserDto
    {
        public UserDto(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.RegistrationTimestamp = user.RegistrationTimestamp;
            this.OrderIds = user.Orders.Select(order => order.Id).ToArray();
            this.IsAdmin = user.IsAdmin;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long RegistrationTimestamp { get; set; }
        public int[] OrderIds { get; set; }
        public bool IsAdmin { get; set; }
    }
}
