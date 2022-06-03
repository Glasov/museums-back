using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;
using WebApplication2.Data.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionController : ControllerBase
    {
        private readonly ExhibitionService exhibitionService;

        public ExhibitionController(ExhibitionService context)
        {
            exhibitionService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExhibitionDto>>> GetAllExhibitions()
        {
            return await exhibitionService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExhibitionDto?>> GetExhibition(int id)
        {
            var exhibition = await exhibitionService.GetById(id);

            if (exhibition == null)
            {
                return NotFound();
            }

            return exhibition;
        }

        [HttpPut]
        public async Task<ActionResult<ExhibitionDto>> UpdateExhibition([FromBody] ExhibitionDto exhibition)
        {
            var result = await exhibitionService.Update(exhibition);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ExhibitionDto>> AddExhibition([FromBody] ExhibitionDto exhibition)
        {
            var result = await exhibitionService.Add(exhibition);
            if (result == null)
            {
                BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibition(int id)
        {
            var exhibition = await exhibitionService.Delete(id);
            if (exhibition)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
