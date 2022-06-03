using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;

namespace WebApplication2.Data.Services
{
    public class ExhibitionItemsService
    {
        private EducationContext context;

        public ExhibitionItemsService(EducationContext context)
        {
            this.context = context;
        }

        public async Task<ExhibitionItemDto?> Add(ExhibitionItemDto exhibitionItemDto)
        {
            ExhibitionItem exhibitionItem = new()
            {
                Name = exhibitionItemDto.Name,
                Description = exhibitionItemDto.Description,
                FullDescription = exhibitionItemDto.FullDescription,
                ImageUrl = exhibitionItemDto.ImageUrl,
            };

            exhibitionItem.Exhibition = context.Exhibitions.FirstOrDefault(e => e.Id == exhibitionItemDto.ExhibitionId);

            var result = context.ExhibitionItems.Add(exhibitionItem);
            await context.SaveChangesAsync();

            return await Task.FromResult(result.Entity != null ? new ExhibitionItemDto(result.Entity) : null);
        }

        public async Task<List<ExhibitionItemDto>> GetAll()
        {
            var result = await context.ExhibitionItems.Include(e => e.Exhibition).Select(e => new ExhibitionItemDto(e)).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<ExhibitionItemDto?> GetById(int id)
        {
            var result = context.ExhibitionItems.Include(e => e.Exhibition).FirstOrDefault(eI => eI.Id == id);
            return await Task.FromResult(result != null ? new ExhibitionItemDto(result) : null);
        }
        
        public async Task<ExhibitionItemDto?> Update(ExhibitionItemDto updatedExhibitionItem)
        {
            var exhibitionItem = await context.ExhibitionItems.Include(e => e.Exhibition).FirstOrDefaultAsync(m => m.Id == updatedExhibitionItem.Id);

            if (exhibitionItem != null)
            {
                exhibitionItem.Name = updatedExhibitionItem.Name;
                exhibitionItem.Description = updatedExhibitionItem.Description;
                exhibitionItem.FullDescription = updatedExhibitionItem.FullDescription;
                exhibitionItem.ImageUrl = updatedExhibitionItem.ImageUrl;

                context.ExhibitionItems.Update(exhibitionItem);
                context.Entry(exhibitionItem).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return exhibitionItem != null ? new ExhibitionItemDto(exhibitionItem) : null;
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var exhibitionItem = await context.ExhibitionItems.FirstOrDefaultAsync(e => e.Id == id);
            if (exhibitionItem != null)
            {
                context.ExhibitionItems.Remove(exhibitionItem);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<ExhibitionItemDto>> GetWithOffset(int count, int offset)
        {
            var exhibitionItems = context.ExhibitionItems.Include(e => e.Exhibition).Skip(offset).Take(count);
            return exhibitionItems.Select(ei => new ExhibitionItemDto(ei)).ToList();
        }
    }
}
