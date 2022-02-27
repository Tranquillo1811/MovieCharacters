using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public class Character : ICharacter
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
    }
}
