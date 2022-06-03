using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersService usersService;

        public UsersController(UsersService context)
        {
            usersService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return await usersService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto?>> GetUser(int id)
        {
            var user = await usersService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut]
        public async Task<ActionResult<UserDto?>> UpdateUser([FromBody] UserUpdateDto user)
        {
            var result = await usersService.Update(user);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<UserDto?> Register([FromBody] UserCreationDto user)
        {
            var result = await usersService.Add(user);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var order = await usersService.Delete(id);
            if (order)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<UserDto?> Login([FromBody] UserLoginDto userLoginDto)
        {
            return await usersService.Login(userLoginDto);
        }
    }
}
