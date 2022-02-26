using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public class Movie
    {
        /// <summary>
        /// PrimaryKey
        /// </summary>
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Genres { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [MaxLength(100)]
        public string Director { get; set; }
        [MaxLength(255)]
        public string MoviePosterUrl { get; set; }
        [MaxLength(255)]
        public string TrailerUrl { get; set; }

        /// <summary>
        /// many-to-many relationship between Movies and characters
        /// One movie contains many characters and a character can play in multiple movies
        /// </summary>
        public ICollection<Character> Characters { get; set; }

        /// <summary>
        /// one-to-many relationship between franchise and movie
        /// One movie belongs to one franchise, but a franchise can contain many movies
        /// </summary>
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
    }
}
