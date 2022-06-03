using WebApplication2.Data.Models;

namespace WebApplication2.Data.Dtos
{
    public class ExhibitionItemDto
    {
        public ExhibitionItemDto(ExhibitionItem exhibitionItem)
        {
            Id = exhibitionItem.Id;
            Name = exhibitionItem.Name;
            Description = exhibitionItem.Description;
            FullDescription = exhibitionItem.FullDescription;
            ImageUrl = exhibitionItem.ImageUrl;
            ExhibitionId = exhibitionItem.Exhibition.Id;
        }
        public int Id { get; set; }
        public int ExhibitionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string ImageUrl { get; set; }
    }
}
