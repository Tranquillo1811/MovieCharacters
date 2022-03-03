using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using MovieCharacters.BLL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieCharacters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all movies
        /// </summary>
        /// <returns>List of each movie details</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAsync()
        {
            List<MovieDto> movies = new();
            var movieEntities = await _movieRepository.GetAllAsync();
            movies = _mapper.Map<List<MovieDto>>(movieEntities);
            return Ok(movies);
        }

        /// <summary>
        /// Get a specific movie from database
        /// </summary>
        /// <param name="id">Movie unique id</param>
        /// <returns>Movie details as a class object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<MovieDto> GetAsync(int id)
        {
            MovieDto movie = _mapper.Map<MovieDto>(await _movieRepository.GetByIdAsync(id));
            return movie;
        }

        /// <summary>
        /// Add a new movie to the database
        /// </summary>
        /// <param name="value">Movie object with all details</param>
        /// <returns>True if a movie was inserted successfully</returns>
        [HttpPost]
        public async Task<ActionResult<MovieDto>> Post([FromBody] MovieDto value)
        {
            Movie movie = _mapper.Map<Movie>(value);
            Movie result = await _movieRepository.AddAsync(movie);
            MovieDto resultDto = _mapper.Map<MovieDto>(result);
            return Ok(resultDto);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
