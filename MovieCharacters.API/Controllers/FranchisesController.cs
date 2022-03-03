using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
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
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAsync()
        {
            List<FranchiseDto> franchises = new();
            var franchiseEntities = await _franchiseRepository.GetAllAsync();
            franchises = _mapper.Map<List<FranchiseDto>>(franchiseEntities);
            return Ok(franchises);
        }

        /// <summary>
        /// Get a specific franchise from database
        /// </summary>
        /// <param name="id">Franchise unique id</param>
        /// <returns>Franchise details as a class object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<FranchiseDto> GetAsync(int id)
        {
            FranchiseDto franchise = _mapper.Map<FranchiseDto>(await _franchiseRepository.GetByIdAsync(id));
            return franchise;
        }

        /// <summary>
        /// Add a new franchise to the database 
        /// </summary>
        /// <param name="value">Franchise object with all details</param>
        /// <returns>True if a franchise was inserted successfully</returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseDto>> Post([FromBody] FranchiseDto value)
        {
            Franchise franchise = _mapper.Map<Franchise>(value);
            Franchise result = await _franchiseRepository.AddAsync(franchise);
            FranchiseDto resultDto = _mapper.Map<FranchiseDto>(result);
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
        public async Task<ActionResult> Put(int id, [FromBody] FranchiseDto value)
        {
            if (id != value.Id)
                return BadRequest();

            Franchise franchise = _mapper.Map<Franchise>(value);
            Franchise franchiseCharacter = await _franchiseRepository.GetByIdAsync(id);
            if (franchiseCharacter == null)   //--- if characterId doesn't exist
                return NotFound();
            if (franchiseCharacter.Equals(franchise))  //--- if nothing was actually changed
            {
                CharacterReadDto characterDto = _mapper.Map<CharacterReadDto>(franchise);
                return StatusCode(StatusCodes.Status304NotModified, characterDto);
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
            Franchise characterBll = await _franchiseRepository.GetByIdAsync(id);

            if (characterBll == null)
                return NoContent();

            await _franchiseRepository.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
