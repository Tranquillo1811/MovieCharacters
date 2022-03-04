using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using MovieCharacters.BLL.Models;

namespace MovieCharacters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseRepository _franchiseRepository;
        private readonly IMapper _mapper;

        public FranchisesController(IFranchiseRepository franchiseRepository, IMapper mapper)
        {
            _franchiseRepository = franchiseRepository;
            _mapper = mapper;
        }

        #region generic CRUD endpoints
        /// <summary>
        /// Get a list of all franchises
        /// </summary>
        /// <returns>List of each franchise details</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FranchiseReadDto>>> GetAsync()
        {
            List<FranchiseReadDto> franchises;
            var franchiseBLL = await _franchiseRepository.GetAllAsync();
            if (franchiseBLL == null)
                return NotFound();
            franchises = _mapper.Map<List<FranchiseReadDto>>(franchiseBLL);
            return Ok(franchises);
        }

        /// <summary>
        /// Get a specific franchise from database
        /// </summary>
        /// <param name="id">Franchise unique id</param>
        /// <returns>Franchise details as a class object</returns>
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Text.Plain)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FranchiseReadDto>> GetAsyncById(int id)
        {
            FranchiseReadDto franchise;
            Franchise franchiseBLL = await _franchiseRepository.GetByIdAsync(id);
            if (franchiseBLL == null)
                return NotFound();
            franchise = _mapper.Map<FranchiseReadDto>(await _franchiseRepository.GetByIdAsync(id));
            return Ok(franchise);
        }

        /// <summary>
        /// Add a new franchise to the database 
        /// </summary>
        /// <param name="value">Franchise object with all details</param>
        /// <returns>True if a franchise was inserted successfully</returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseReadDto>> Post([FromBody] FranchiseReadDto value)
        {
            Franchise franchise = _mapper.Map<Franchise>(value);
            Franchise result = await _franchiseRepository.AddAsync(franchise);
            FranchiseReadDto resultDto = _mapper.Map<FranchiseReadDto>(result);
            return Ok(resultDto);
        }

        /// <summary>
        /// updates existing Franchise in the Db
        /// </summary>
        /// <param name="id">id of the franchise that is subject to change</param>
        /// <param name="value">JSON of updated character object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] FranchiseReadDto value)
        {
            if (id != value.Id)
                return BadRequest();

            Franchise franchise = _mapper.Map<Franchise>(value);
            Franchise franchiseCharacter = await _franchiseRepository.GetByIdAsync(id);
            if (franchiseCharacter == null)   //--- if franchiseId doesn't exist
                return NotFound();
            if (franchiseCharacter.Equals(franchise))  //--- if nothing was actually changed
            {
                FranchiseReadDto franchiseDto = _mapper.Map<FranchiseReadDto>(franchise);
                return StatusCode(StatusCodes.Status304NotModified, franchiseDto);
            }
            await _franchiseRepository.UpdateAsync(franchise);
            return NoContent();
        }

        /// <summary>
        /// deletes the franchise with the respective Id
        /// </summary>
        /// <param name="id">Id of the franchise to be deleted</param>
        /// <returns>200 if character has been deleted or 204 if character wasn't present at all</returns>
        [Consumes(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            Franchise franchiseBll = await _franchiseRepository.GetByIdAsync(id);

            if (franchiseBll == null)
                return NoContent();

            await _franchiseRepository.DeleteByIdAsync(id);

            return Ok();
        }
        #endregion

        #region update movies in a franchise
        /// <summary>
        /// updates movies of a franchise
        /// </summary>
        /// <param name="franchiseId">id of the franchise to change movies of</param>
        /// <param name="moviesIds">ids of movies to set</param>
        /// <returns></returns>
        [HttpPatch("{movieId}/Characters")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Patch(int franchiseId, [FromBody] int[] moviesIds)
        {
            Franchise franchiseBll = await _franchiseRepository.GetByIdAsync(franchiseId);
            if (franchiseBll == null)   //--- if franchiseId doesn't exist
                return NotFound();
            int[] currentFranchiseMovieIds = franchiseBll.Movies.Select(m => m.Id).ToArray();
            if (string.Join(',', currentFranchiseMovieIds.OrderBy(id => id)) == string.Join(',', moviesIds.OrderBy(id => id)))  //--- if nothing was actually changed
            {
                FranchiseReadDto movieDto = _mapper.Map<FranchiseReadDto>(franchiseBll);
                return StatusCode(StatusCodes.Status304NotModified, movieDto);
            }
            await _franchiseRepository.SetMovieIdsAsync(franchiseBll, moviesIds);
            return NoContent();
        }
        #endregion

        #region reports for movies and characters
        /// <summary>
        /// Report all movies from franchise
        /// </summary>
        /// <param name="id">Id of the franchise being searched for</param>
        /// <returns>A list of all movies assigned to that franchise</returns>
        [HttpGet("ReportMovies/{id}")]
        [Consumes(MediaTypeNames.Text.Plain)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetMoviesByFranchiseId(int id)
        {
            List<MovieReadDto> movieReadDtos;
            IEnumerable<Movie> movieBlls = await _franchiseRepository.GetMoviesById(id);
            if (movieBlls == null)
                return NotFound();
            movieReadDtos = _mapper.Map<List<MovieReadDto>>(movieBlls);
            return Ok(movieReadDtos);
        }

        /// <summary>
        /// Report all characters from franchise
        /// </summary>
        /// <param name="id">Id of the franchise being searched for</param>
        /// <returns>A list of all characters assigned to that franchise</returns>
        [HttpGet("ReportCharacters/{id}")]
        [Consumes(MediaTypeNames.Text.Plain)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CharacterReadDto>>> GetCharactersByFranchiseId(int id)
        {
            List<CharacterReadDto> characterReadDtos;
            IEnumerable<Character> characterBlls = await _franchiseRepository.GetCharactersById(id);
            if (characterBlls == null)
                return NotFound();
            characterReadDtos = _mapper.Map<List<CharacterReadDto>>(characterBlls);
            return Ok(characterReadDtos);
        }
        #endregion
    }
}
