using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controller
{
    [Route("api/[controller]")]
    //[ApiController]
    public class MagicVillaController : ControllerBase
    {
        [HttpGet("GetVilla")]
        public ActionResult<IEnumerable<VillaDto>> GetVilla()
        {
            return Ok(VillaStore.GetVillas().Select(villa => new VillaDto()
            {
                Id = villa.Id,
                Name = villa.Name
            }));
        }

        [HttpGet("GetVilla/{id:int}", Name = "GetVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public ActionResult GetVilla(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.GetVillas().FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            var villaDto = new VillaDto()
            {
                Id = villa.Id,
                Name = "9999"
            };

            return Ok(villaDto);
        }

        [HttpPost("CreateVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult CreateVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (VillaStore.GetVillas().Any(x => x.Name.ToLower().Equals(villaDto.Name.ToLower())))
            {
                ModelState.AddModelError("", "Villa already exists");
                return BadRequest(ModelState);
            }

            var villa = new Villa()
            {
                Id = VillaStore.GetVillas().Max(x => x.Id) + 1,
                Name = villaDto.Name
            };

            VillaStore.GetVillas().Add(villa);

            return CreatedAtRoute("GetVilla", new {villaDto.Id}, villaDto);
        }


        [HttpDelete("UpdateVilla/{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.GetVillas().FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.GetVillas().Remove(villa);

            return NoContent();
        }

        [HttpPut("UpdateVilla/{id:int}")]
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

            var villa = VillaStore.GetVillas().FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            villa.Name = villaDto.Name;

            return NoContent();
        }
    }
}