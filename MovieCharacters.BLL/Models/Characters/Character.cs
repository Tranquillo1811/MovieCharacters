using MovieCharacters.BLL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacters.BLL.Models
{
    public class Character
    {
        /// <summary>
        /// PrimaryKey
        /// </summary>
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(255)]
        public string Alias { get; set; }
        public string Gender { get; set; }
        [MaxLength(255)]
        public string PictureUrl { get; set; }

        /// <summary>
        /// many-to-many relationship between Movies and characters
        /// One movie contains many characters and a character can play in multiple movies
        /// </summary>
        public ICollection<Movie> Movies { get; set;}
        public override bool Equals(object obj)
        {
            Character character = obj as Character;
            if(character == null)
                return false;
            if
                (
                    Id != character.Id
                    ||
                    FullName != character.FullName
                    ||
                    Alias != character.Alias
                    ||
                    Gender != character.Gender
                    ||
                    PictureUrl != character.PictureUrl
                )
                return false;
            //TODO: add check whether character.Movies contains same movies as Movies
            return true;

        }
    }
}
