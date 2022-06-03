using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;
using WebApplication2.Data.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService ordersService;

        public OrdersController(OrdersService context)
        {
            ordersService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            return await ordersService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto?>> GetOrder(int id)
        {
            var order = await ordersService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut]
        public async Task<OrderDto?> UpdateOrder([FromBody] OrderDto order)
        {
            var result = await ordersService.Update(order);
            return result;
        }

        [HttpPost]
        public async Task<OrderDto?> AddOrder([FromBody] OrderCreationDto order)
        {
            var result = await ordersService.Add(order);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await ordersService.Delete(id);
            if (order)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
