using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    internal class Character
    {
        /// <summary>
        /// PrimaryKey
        /// </summary>
        int Id { get; set; }

        [Required]
        [MaxLength(100)]
        string FullName { get; set; }
        [MaxLength(255)]
        string Alias { get; set; }
        string Gender { get; set; }
        [MaxLength(255)]
        string PictureUrl { get; set; }

        /// <summary>
        /// many-to-many relationship between Movies and characters
        /// One movie contains many characters and a character can play in multiple movies
        /// </summary>
        ICollection<Movie> Movies { get; set;}
    }
}
