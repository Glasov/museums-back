using WebApplication2.Data.Models;

namespace WebApplication2.Data.Dtos
{
    public class ExhibitionDto
    {
        public ExhibitionDto(Exhibition exhibition)
        {
            Id = exhibition.Id;
            Museum = new MuseumDto(exhibition.Museum);
            Name = exhibition.Name;
            Description = exhibition.Description;
            StartTimestamp = exhibition.StartTimestamp;
            EndTimestamp = exhibition.EndTimestamp;
            Cost = exhibition.Cost;
            ExhibitionItemIds = exhibition.ExhibitionItems.Select(x => x.Id).ToArray();
            OrderIds = exhibition.Orders.Select(x => x.Id).ToArray();
        }
        public int Id { get; set; }
        public MuseumDto Museum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long StartTimestamp { get; set; }
        public long EndTimestamp { get; set; }
        public double Cost { get; set; }
        public int[] ExhibitionItemIds { get; set; }
        public int[] OrderIds { get; set; }
    }
}
