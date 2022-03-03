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
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseRepository _franchiseRepository;
        private readonly IMapper _mapper;

        public FranchisesController(IFranchiseRepository franchiseRepository, IMapper mapper)
        {
            _franchiseRepository = franchiseRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all franchises
        /// </summary>
        /// <returns>List of each franchise details</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<FranchiseReadDto>>> GetAsync()
        {
            List<FranchiseReadDto> franchises = new();
            var franchiseEntities = await _franchiseRepository.GetAllAsync();
            franchises = _mapper.Map<List<FranchiseReadDto>>(franchiseEntities);
            return Ok(franchises);
        }

        /// <summary>
        /// Get a specific franchise from database
        /// </summary>
        /// <param name="id">Franchise unique id</param>
        /// <returns>Franchise details as a class object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<FranchiseReadDto> GetAsync(int id)
        {
            FranchiseReadDto franchise = _mapper.Map<FranchiseReadDto>(await _franchiseRepository.GetByIdAsync(id));
            return franchise;
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

        // PUT api/<FranchisesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FranchisesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
