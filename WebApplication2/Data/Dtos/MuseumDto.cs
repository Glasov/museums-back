using WebApplication2.Data.Models;

namespace WebApplication2.Data.Dtos
{
    public class MuseumDto {
        public MuseumDto(Museum museum)
        {
            Id = museum.Id;
            Name = museum.Name;
            Description = museum.Description;
            ExhibitionIds = museum.Exhibitions.Select(x => x.Id).ToArray();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] ExhibitionIds { get; set; }
    }
}
