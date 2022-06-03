using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Dtos;
using WebApplication2.Data.Models;

namespace WebApplication2.Data.Services
{
    public class MuseumsService
    {
        private EducationContext context;
        
        public MuseumsService(EducationContext context)
        {
            this.context = context;
        }

        public async Task<Museum?> Add(MuseumDto museumDto)
        {
            Museum museum = new()
            {
                Name = museumDto.Name,
                Description = museumDto.Description
            };

            if (museumDto.ExhibitionIds.Any())
            {
                museum.Exhibitions = context.Exhibitions.ToList().IntersectBy(museumDto.ExhibitionIds, e => e.Id).ToList();
            }

            var result = context.Museums.Add(museum);
            await context.SaveChangesAsync();

            return await Task.FromResult(result.Entity);
        }

        public async Task<List<MuseumDto>> GetAll()
        {
            var result = await context.Museums.Include(m => m.Exhibitions).Select(museum => new MuseumDto(museum)).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<MuseumDto?> GetById(int id)
        {
            var result = context.Museums.Include(m => m.Exhibitions).FirstOrDefault(m => m.Id == id);
            return await Task.FromResult(result != null ? new MuseumDto(result) : null);
        }

        public async Task<MuseumDto?> Update(MuseumDto updatedMuseum)
        {
            var museum = await context.Museums.Include(m => m.Exhibitions).FirstOrDefaultAsync(m => m.Id == updatedMuseum.Id);

            if (museum != null)
            {
                museum.Name = updatedMuseum.Name;
                museum.Description = updatedMuseum.Description;

                if (updatedMuseum.ExhibitionIds.Any())
                {
                    museum.Exhibitions = context.Exhibitions.ToList().IntersectBy(updatedMuseum.ExhibitionIds, e => e.Id).ToList();
                }

                context.Museums.Update(museum);
                context.Entry(museum).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return museum != null ? new MuseumDto(museum) : null;
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var museum = await context.Museums.FirstOrDefaultAsync(m => m.Id == id);
            if (museum != null)
            {
                context.Museums.Remove(museum);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
