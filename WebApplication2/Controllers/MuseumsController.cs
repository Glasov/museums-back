using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;
using WebApplication2.Data.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuseumsController : ControllerBase
    {
        private readonly MuseumsService museumsService;

        public MuseumsController(MuseumsService context)
        {
            museumsService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MuseumDto>>> GetAllOrders()
        {
            return await museumsService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MuseumDto?>> GetMuseum(int id)
        {
            var order = await museumsService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut]
        public async Task<ActionResult<MuseumDto>> UpdateMuseum([FromBody] MuseumDto museum)
        {
            var result = await museumsService.Update(museum);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MuseumDto>> AddMuseum([FromBody] MuseumDto museum)
        {
            var result = await museumsService.Add(museum);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMuseum(int id)
        {
            var order = await museumsService.Delete(id);
            if (order)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
