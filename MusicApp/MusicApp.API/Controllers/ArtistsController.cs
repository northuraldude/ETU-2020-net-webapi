using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MusicApp.API.Resources;
using MusicApp.Core.Models;
using MusicApp.Core.Services;

namespace MusicApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<SaveArtistResource> _validator;
        
        public ArtistsController(IArtistService artistService, IMapper mapper, AbstractValidator<SaveArtistResource> validator)
        {
            _mapper = mapper;
            _artistService = artistService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistResource>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();
            var artistResources = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistResource>>(artists);

            return Ok(artistResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistResource>> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            var artistResource = _mapper.Map<Artist, ArtistResource>(artist);

            return Ok(artistResource);
        }

        [HttpPost]
        public async Task<ActionResult<ArtistResource>> CreateArtist([FromBody] SaveArtistResource saveArtistResource)
        {
            var validationResult = await _validator.ValidateAsync(saveArtistResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var artistToCreate = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);
            var newArtist = await _artistService.CreateArtist(artistToCreate);
            var artistResource = _mapper.Map<Artist, ArtistResource>(newArtist);

            return Ok(artistResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistResource>> UpdateArtist(int id, [FromBody] SaveArtistResource saveArtistResource)
        {
            var validationResult = await _validator.ValidateAsync(saveArtistResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var artist = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);

            await _artistService.UpdateArtist(id, artist);

            var updatedArtist = await _artistService.GetArtistById(id);
            var updatedArtistResource = _mapper.Map<Artist, ArtistResource>(updatedArtist);

            return Ok(updatedArtistResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _artistService.GetArtistById(id);

            await _artistService.DeleteArtist(artist);
            
            return NoContent();
        }
    }
}