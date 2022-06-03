using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;

namespace WebApplication2.Data.Services
{
    public class UsersService
    {
        private static SHA256 SHA256_ENCODER = SHA256.Create();
        private EducationContext context;

        public UsersService(EducationContext context)
        {
            this.context = context;
        }

        public async Task<UserDto?> Add(UserCreationDto userDto)
        {
            Console.Out.WriteLine("registration: got dto: " + userDto.Email + " " + userDto.Name + " " + userDto.Password);
            if (context.Users.FirstOrDefault(u => u.Email == userDto.Email) != null)
            {
                Console.Out.WriteLine("failed to register: email presented");
                return null;
            }
            else
            {
                Console.Out.WriteLine("register: not found such email. good!");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(userDto.Password);
            User user = new()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = Encoding.UTF8.GetString(SHA256_ENCODER.ComputeHash(bytes)),
                RegistrationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                LastEditTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Orders = new List<Order>()
            };

            Console.Out.WriteLine("created model");

            var result = context.Users.Add(user);
            await context.SaveChangesAsync();

            return await Task.FromResult(result.Entity != null ? new UserDto(result.Entity) : null);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var result = await context.Users.Include(o => o.Orders).Select(user => new UserDto(user)).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<UserDto?> GetById(int id)
        {
            var result = context.Users.Include(o => o.Orders).FirstOrDefault(u => u.Id == id);
            return await Task.FromResult(result != null ? new UserDto(result) : null);
        }

        public async Task<UserDto?> Update(UserUpdateDto updatedUser)
        {
            var user = await context.Users.Include(o => o.Orders).FirstOrDefaultAsync(m => m.Id == updatedUser.Id);
            byte[] bytes = Encoding.UTF8.GetBytes(updatedUser.Password);
            
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.PasswordHash = Encoding.UTF8.GetString(SHA256_ENCODER.ComputeHash(bytes));
                user.LastEditTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                context.Users.Update(user);
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return new UserDto(user);
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task<User?> GetByEmail(string email)
        {
            return await context.Users.Include(e => e.Orders).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserDto?> Login(UserLoginDto userLoginDto)
        {
            User? user = await GetByEmail(userLoginDto.Email);

            if (user == null)
            {
                Console.Out.WriteLine("user not found");
                return null;
            }

            Console.Out.WriteLine("found user! " + user.Id + " " + user.Email + " " + user.Name + " " + user.IsAdmin + " " + user.PasswordHash);
            
            byte[] bytes = Encoding.UTF8.GetBytes(userLoginDto.Password);
            if (user.PasswordHash != Encoding.UTF8.GetString(SHA256_ENCODER.ComputeHash(bytes)))
            {
                Console.Out.WriteLine("password hashes are not the same!");
                return null;
            }

            return new UserDto(user);
        }
    }
}
