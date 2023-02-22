using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Log;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controller
{
    [Route("api/[controller]")]
    //[ApiController]
    public class MagicVillaController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly ApplicationDBContext _dbContext;

        public MagicVillaController(ILogging logger, ApplicationDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet()]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(VillaDto))]
        public IActionResult GetVilla()
        {
            _logger.LogInformation("Get All Villas");
            return Ok(_dbContext.Villas.Select(villa => new VillaDto()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                Location = villa.Location,
                Price = villa.Price,
                Rating = villa.Rating,
                ImageUrl = villa.ImageUrl
            }));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult GetVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("id <=0)");
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            var villaDto = new VillaDto()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                Location = villa.Location,
                Price = villa.Price,
                Rating = villa.Rating,
                ImageUrl = villa.ImageUrl
            };

            return Ok(villaDto);
        }

        [HttpPost()]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult CreateVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dbContext.Villas.Any(x => x.Name.ToLower().Equals(villaDto.Name.ToLower())))
            {
                ModelState.AddModelError("", "Villa already exists");
                return BadRequest(ModelState);
            }

            var villa = new Villa()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Description = villaDto.Description,
                Location = villaDto.Location,
                Price = villaDto.Price,
                Rating = villaDto.Rating,
                ImageUrl = villaDto.ImageUrl,
                CreatedDate = DateTime.Now,
            };

            _dbContext.Add(villa);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetVilla", new {villaDto.Id}, villaDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _dbContext.Remove(villa);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0 || villaDto.Id != id)
            {
                return BadRequest();
            }

            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            villa.Id = villaDto.Id;
            villa.Name = villaDto.Name;
            villa.Description = villaDto.Description;
            villa.Location = villaDto.Location;
            villa.Price = villaDto.Price;
            villa.Rating = villaDto.Rating;
            villa.ImageUrl = villaDto.ImageUrl;
            villa.UpdatedDate = DateTime.Now;
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] JsonPatchDocument<VillaDto> patchDoc)
        {
            var villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            var villaDto = new VillaDto()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                Location = villa.Location,
                Price = villa.Price,
                Rating = villa.Rating,
                ImageUrl = villa.ImageUrl
            };

            patchDoc.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(villaDto))
            {
                return BadRequest(ModelState);
            }

            villa.Id = villaDto.Id;
            villa.Name = villaDto.Name;
            villa.Description = villaDto.Description;
            villa.Location = villaDto.Location;
            villa.Price = villaDto.Price;
            villa.Rating = villaDto.Rating;
            villa.ImageUrl = villaDto.ImageUrl;
            villa.UpdatedDate = DateTime.Now;
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}