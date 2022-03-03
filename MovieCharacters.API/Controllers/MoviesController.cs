using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using MovieCharacters.BLL.Models;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieCharacters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, ICharacterRepository characterRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        #region generic CRUD endpoints
        /// <summary>
        /// Get a list of all movies
        /// </summary>
        /// <returns>List of each movie details</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetAsync()
        {
            List<MovieReadDto> movies;
            var moviesBLL = await _movieRepository.GetAllAsync();
            if (moviesBLL == null)
                return NoContent();
            movies = _mapper.Map<List<MovieReadDto>>(moviesBLL);
            return Ok(movies);
        }

        /// <summary>
        /// Get a specific movie from database
        /// </summary>
        /// <param name="id">Movie unique id</param>
        /// <returns>Movie details as a class object</returns>
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Text.Plain)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReadDto>> GetAsyncById(int id)
        {
            MovieReadDto movie;
            Movie movieBLL = await _movieRepository.GetByIdAsync(id);
            if (movieBLL == null)
                return NotFound();
            movie = _mapper.Map<MovieReadDto>(await _movieRepository.GetByIdAsync(id));
            return Ok(movie);
        }

        /// <summary>
        /// Add a new movie to the database
        /// </summary>
        /// <param name="value">Movie object with all details</param>
        /// <returns>True if a movie was inserted successfully</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MovieReadDto>> Post([FromBody] MovieAddDto value)
        {
            Movie movie = _mapper.Map<Movie>(value);
            Movie result = await _movieRepository.AddAsync(movie);
            MovieReadDto resultDto = _mapper.Map<MovieReadDto>(result);
            return CreatedAtAction(nameof(GetAsyncById), new { id = result.Id }, resultDto);
        }

        // PUT api/<MoviesController>/5
        /// <summary>
        /// updates existing movie in the Db
        /// </summary>
        /// <param name="id">id of the movie that is subject to change</param>
        /// <param name="value">JSON of updated movie object</param>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] MovieUpdateDto value)
        {
            if (id != value.Id)
                return BadRequest();
            Movie movieBll = _mapper.Map<Movie>(value);
            Movie currentMovie = await _movieRepository.GetByIdAsync(id);
            if (currentMovie == null)   //--- if movieId doesn't exist
                return NotFound();
            if (currentMovie.Equals(movieBll))  //--- if nothing was actually changed
            {
                MovieReadDto movieDto = _mapper.Map<MovieReadDto>(movieBll);
                return StatusCode(StatusCodes.Status304NotModified, movieDto);
            }
            await _movieRepository.UpdateAsync(movieBll);
            return NoContent();
        }

        // DELETE api/<MoviesController>/5
        /// <summary>
        /// deletes the movie with the respective Id from Movies Db table
        /// </summary>
        /// <param name="id">Id of the movie to be deleted</param>
        /// <returns>200 if movie has been deleted or 204 if movie wasn't present at all</returns>
        [Consumes(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            Movie movieBll = await _movieRepository.GetByIdAsync(id);
            if (movieBll == null)
                return NoContent();
            await _movieRepository.DeleteByIdAsync(id);
            return Ok();
        }
        #endregion

        #region update characters in a movie
        /// <summary>
        /// updates characters of a movie
        /// </summary>
        /// <param name="movieId">id of the movie to change characters of</param>
        /// <param name="characterIds">ids of characters to set</param>
        /// <returns></returns>
        [HttpPatch("{movieId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Patch(int movieId, [FromBody] int[] characterIds)
        {
            Movie movieBll = await _movieRepository.GetByIdAsync(movieId);
            if (movieBll == null)   //--- if movieId doesn't exist
                return NotFound();
            int[] currentMoviesCharacterIds = movieBll.Characters.Select(c => c.Id).ToArray();
            if (string.Join(',',currentMoviesCharacterIds.OrderBy(id => id)) == string.Join(',', characterIds.OrderBy(id => id)))  //--- if nothing was actually changed
            {
                MovieReadDto movieDto = _mapper.Map<MovieReadDto>(movieBll);
                return StatusCode(StatusCodes.Status304NotModified, movieDto);
            }
            await _movieRepository.SetCharacterIdsAsync(movieBll, characterIds);
            return NoContent();
        }
        #endregion

    }
}
