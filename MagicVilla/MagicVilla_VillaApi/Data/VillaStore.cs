using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Data
{
    public static class VillaStore
    {
        private readonly static List<Villa> _villas = new()
        {
            new Villa()
            {
                Id = 1,
                Name = "Villa 1",
                Description = "Villa 1 Description",
                Location = "Villa 1 Location",
                Price = 100,
                Rating = 5,
                ImageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 2,
                Name = "Villa 2",
                Description = "Villa 2 Description",
                Location = "Villa 2 Location",
                Price = 200,
                Rating = 4,
                ImageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 3,
                Name = "Villa 3",
                Description = "Villa 3 Description",
                Location = "Villa 3 Location",
                Price = 300,
                Rating = 3,
                ImageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 4,
                Name = "Villa 4",
                Description = "Villa 4 Description",
                Location = "Villa 4 Location",
                Price = 400,
                Rating = 2,
                ImageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 5,
                Name = "Villa 5",
                Description = "Villa 5 Description",
                Location = "Villa 5 Location",
                Price = 500,
                Rating = 1,
                ImageUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                CreatedDate = DateTime.Now
            },
        };

        public static List<Villa> GetVillas()
        {
            return _villas;
        }
    }
}