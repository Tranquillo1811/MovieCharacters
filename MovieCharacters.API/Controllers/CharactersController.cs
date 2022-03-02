using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacters.BLL.Models;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieCharacters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        public CharactersController(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a full list of Characters from Db
        /// </summary>
        /// <returns>JSON CharactersArray with all CharacterObjects from the Db</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAsync()
        {
            List<CharacterDto> characters;
            var charactersBLL = await _characterRepository.GetAllAsync();
            if (charactersBLL == null)
                return NoContent();
            characters = _mapper.Map<List<CharacterDto>>(charactersBLL);
            return Ok(characters);
        }

        /// <summary>
        /// Gets Character with a particular id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON CharacterObject with respective id</returns>
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Text.Plain)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CharacterDto>> GetAsyncById(int id)
        {
            CharacterDto character;
            Character characterBLL = await _characterRepository.GetByIdAsync(id);
            if(characterBLL == null)
                return NotFound();
            character = _mapper.Map<CharacterDto>(await _characterRepository.GetByIdAsync(id));
            return Ok(character);
        }

        /// <summary>
        /// Adds new Character to the Db
        /// </summary>
        /// <param name="value">JSON CharacterObject that describes the new character</param>
        /// <returns>JSON of the newly added characterObject</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CharacterDto>> Post([FromBody] CharacterDto value)
        {
            Character characterBll = _mapper.Map<Character>(value);
            Character result = await _characterRepository.AddAsync(characterBll);
            CharacterDto resultDto = _mapper.Map<CharacterDto>((Character)result);
            return CreatedAtAction(nameof(GetAsyncById), new { id = result.Id }, resultDto);
        }

        // PUT api/<CharactersController>/5
        /// <summary>
        /// updates existing character in the Db
        /// </summary>
        /// <param name="id">id of the character that is subject to change</param>
        /// <param name="value">JSON of updated character object</param>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] CharacterDto value)
        {
            if (id != value.Id)
                return BadRequest();
            Character characterBll = _mapper.Map<Character>(value);
            Character currentCharacter = await _characterRepository.GetByIdAsync(id);
            if (currentCharacter == null)   //--- if characterId doesn't exist
                return NotFound();
            if (currentCharacter.Equals(characterBll))  //--- if nothing was actually changed
            {
                CharacterDto characterDto = _mapper.Map<CharacterDto>(characterBll);
                return StatusCode(StatusCodes.Status304NotModified, characterDto);
            }
            Character result = await _characterRepository.UpdateAsync(characterBll);
            return NoContent();
        }

        // DELETE api/<CharactersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
