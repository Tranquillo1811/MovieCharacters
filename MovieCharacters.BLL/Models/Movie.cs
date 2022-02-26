using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    internal class Movie
    {
        /// <summary>
        /// PrimaryKey
        /// </summary>
        int Id { get; set; }

        [Required]
        [MaxLength(100)]
        string Title { get; set; }
        [MaxLength(255)]
        string Genres { get; set; }
        [Required]
        int ReleaseYear { get; set; }
        [MaxLength(100)]
        string Director { get; set; }
        [MaxLength(255)]
        string MoviePosterUrl { get; set; }
        [MaxLength(255)]
        string TrailerUrl { get; set; }

        /// <summary>
        /// many-to-many relationship between Movies and characters
        /// One movie contains many characters and a character can play in multiple movies
        /// </summary>
        ICollection<Character> Characters { get; set; }

        /// <summary>
        /// one-to-many relationship between franchise and movie
        /// One movie belongs to one franchise, but a franchise can contain many movies
        /// </summary>
        int FranchiseId { get; set; }
        Franchise Franchise { get; set; }
    }
}
