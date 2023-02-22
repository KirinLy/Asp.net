using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models.Dtos
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
    }
}