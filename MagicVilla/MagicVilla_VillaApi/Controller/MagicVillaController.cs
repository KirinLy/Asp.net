using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Log;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controller
{
    [Route("api/[controller]")]
    //[ApiController]
    public class MagicVillaController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogging _logger;

        public MagicVillaController(ApplicationDBContext dbContext, IMapper mapper, ILogging logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVilla()
        {
            var villas = await _dbContext.Villas.AsNoTracking().ToListAsync();
            _logger.LogInformation("Get All Villas");
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villas));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("id <=0)");
                return BadRequest();
            }

            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost()]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _dbContext.Villas.AnyAsync(x => x.Name.ToLower().Equals(villaCreateDto.Name.ToLower())))
            {
                ModelState.AddModelError("", "Villa already exists");
                return BadRequest(ModelState);
            }

            var villa = _mapper.Map<Villa>(villaCreateDto);

            await _dbContext.AddAsync(villa);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {villa.Id}, villaCreateDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _dbContext.Remove(villa);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0 || villaUpdateDto.Id != id)
            {
                return BadRequest();
            }

            if (!await _dbContext.Villas.AnyAsync(x => x.Id == id))
            {
                return NotFound();
            }

            var villa = _mapper.Map<Villa>(villaUpdateDto);
            _dbContext.Update(villa);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDto> patchDoc)
        {
            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            var villaUpdateDto = _mapper.Map<VillaUpdateDto>(villa) ;

            patchDoc.ApplyTo(villaUpdateDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(villaUpdateDto))
            {
                return BadRequest(ModelState);
            }

            var villaUpdate = _mapper.Map<Villa>(villaUpdateDto);
            _dbContext.Update(villaUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}