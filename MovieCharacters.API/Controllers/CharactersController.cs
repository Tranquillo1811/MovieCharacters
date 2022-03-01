using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharacters.BLL.Models;
using MovieCharacters.DAL.Models;
using System.Collections.Generic;
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

        // GET: api/<CharactersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAsync()
        {
            List<CharacterDto> characters = new();
            var characterEntities = await _characterRepository.GetAllAsync();
            characters = _mapper.Map<List<CharacterDto>>(characterEntities);
            return Ok(characters);
        }

        // GET api/<CharactersController>/2
        [HttpGet("{id}")]
        public async Task<CharacterDto> GetAsync(int id)
        {
            CharacterDto character;
            character = _mapper.Map<CharacterDto>(await _characterRepository.GetByIdAsync(id));
            return character;
        }

        // POST api/<CharactersController>
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> Post([FromBody] CharacterDto value)
        {
            ICharacter character = _mapper.Map<Character>(value);
            ICharacter result = await _characterRepository.AddAsync(character);
            CharacterDto resultDto = _mapper.Map<CharacterDto>((Character)result);
            return Ok(resultDto);
        }

        // PUT api/<CharactersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CharactersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
