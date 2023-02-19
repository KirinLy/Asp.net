using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models.Dtos
{
    public class VillaDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}