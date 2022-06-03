using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;
using WebApplication2.Data.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionItemsController : ControllerBase
    {
        private readonly ExhibitionItemsService exhibitionItemsService;

        public ExhibitionItemsController(ExhibitionItemsService context)
        {
            exhibitionItemsService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExhibitionItemDto>>> GetAllExhibitionItems()
        {
            return await exhibitionItemsService.GetAll();
        }

        [HttpGet("next/{count}/{offset}")]
        public async Task<ActionResult<IEnumerable<ExhibitionItemDto>>> GetExhibitionItems(int count, int offset)
        {
            return await exhibitionItemsService.GetWithOffset(count, offset);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExhibitionItemDto?>> GetExhibitionItem(int id)
        {
            var exhibitionItem = await exhibitionItemsService.GetById(id);

            if (exhibitionItem == null)
            {
                return NotFound();
            }

            return exhibitionItem;
        }

        [HttpPut]
        public async Task<ActionResult<ExhibitionItemDto>> UpdateExhibitionItem([FromBody] ExhibitionItemDto exhibitionItem)
        {
            var result = await exhibitionItemsService.Update(exhibitionItem);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ExhibitionItem>> AddExhibitionItem([FromBody] ExhibitionItemDto exhibitionItem)
        {
            var result = await exhibitionItemsService.Add(exhibitionItem);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibitionItem(int id)
        {
            var exhibitionItem = await exhibitionItemsService.Delete(id);
            if (exhibitionItem)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
