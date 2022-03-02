using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using MovieCharacters.BLL.Models;
using MovieCharacters.DAL.Models;

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

        // GET api/<FranchisesController>
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAsync()
        {
            List<FranchiseDto> franchises = new();
            var franchiseEntities = await _franchiseRepository.GetAllAsync();
            franchises = _mapper.Map<List<FranchiseDto>>(franchiseEntities);
            return Ok(franchises);
        }

        // GET api/<FranchisesController>/2
        [HttpGet("{id}")]
        public async Task<FranchiseDto> GetAsync(int id)
        {
            FranchiseDto franchise = _mapper.Map<FranchiseDto>(await _franchiseRepository.GetByIdAsync(id));
            return franchise;
        }

        // POST api/<FranchisesController>
        [HttpPost]
        public async Task<ActionResult<FranchiseDto>> Post([FromBody] FranchiseDto value)
        {
            IFranchise franchise = _mapper.Map<Franchise>(value);
            IFranchise result = await _franchiseRepository.AddAsync(franchise);
            FranchiseDto resultDto = _mapper.Map<FranchiseDto>(result);
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
