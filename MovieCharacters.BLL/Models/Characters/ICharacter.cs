using System.Collections.Generic;

namespace MovieCharacters.BLL.Models
{
    public interface ICharacter
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string PictureUrl { get; set; }
    }
}
