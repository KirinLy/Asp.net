using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models.Dtos
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
    }
}