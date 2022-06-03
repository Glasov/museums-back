using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;

namespace WebApplication2.Data.Services
{
    public class ExhibitionService
    {
        private EducationContext context;

        public ExhibitionService(EducationContext context)
        {
            this.context = context;
        }

        public async Task<ExhibitionDto?> Add(ExhibitionDto exhibitionDto)
        {
            Exhibition exhibition = new()
            {
                Name = exhibitionDto.Name,
                Description = exhibitionDto.Description,
                StartTimestamp = exhibitionDto.StartTimestamp,
                EndTimestamp = exhibitionDto.EndTimestamp,
                Cost = exhibitionDto.Cost
            };

            exhibition.Museum = context.Museums.FirstOrDefault(m => m.Id == exhibitionDto.Museum.Id);

            if (exhibitionDto.ExhibitionItemIds.Any())
            {
                exhibition.ExhibitionItems = context.ExhibitionItems.ToList().IntersectBy(exhibitionDto.ExhibitionItemIds, e => e.Id).ToList();
            }

            if (exhibitionDto.OrderIds.Any())
            {
                exhibition.Orders = context.Orders.ToList().IntersectBy(exhibitionDto.OrderIds, o => o.Id).ToList();
            }

            var result = context.Exhibitions.Add(exhibition);
            await context.SaveChangesAsync();

            return await Task.FromResult(result.Entity != null ? new ExhibitionDto(result.Entity) : null);
        }

        public async Task<List<ExhibitionDto>> GetAll()
        {
            var result = await context.Exhibitions.Include(e => e.Museum).Include(e => e.ExhibitionItems).Include(e => e.Orders).Select(e => new ExhibitionDto(e)).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<ExhibitionDto?> GetById(int id)
        {
            var result = context.Exhibitions.Include(e => e.Museum).Include(e => e.Orders).Include(e => e.ExhibitionItems).FirstOrDefault(m => m.Id == id);
            return await Task.FromResult(result != null ? new ExhibitionDto(result) : null);
        }

        public async Task<ExhibitionDto?> Update(ExhibitionDto updatedExhibition)
        {
            var exhibition = await context.Exhibitions.Include(e => e.Museum).Include(e => e.ExhibitionItems).FirstOrDefaultAsync(m => m.Id == updatedExhibition.Id);

            if (exhibition != null)
            {
                exhibition.Name = updatedExhibition.Name;
                exhibition.Description = updatedExhibition.Description;

                if (updatedExhibition.ExhibitionItemIds.Any())
                {
                    exhibition.ExhibitionItems = context.ExhibitionItems.ToList().IntersectBy(updatedExhibition.ExhibitionItemIds, e => e.Id).ToList();
                }

                if (updatedExhibition.OrderIds.Any())
                {
                    exhibition.Orders = context.Orders.ToList().IntersectBy(updatedExhibition.OrderIds, o => o.Id).ToList();
                }

                context.Exhibitions.Update(exhibition);
                context.Entry(exhibition).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return exhibition != null ? new ExhibitionDto(exhibition) : null;
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var exhibition = await context.Exhibitions.FirstOrDefaultAsync(e => e.Id == id);
            if (exhibition != null)
            {
                context.Exhibitions.Remove(exhibition);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
