using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    internal class Franchise
    {
        /// <summary>
        /// PrimaryKey
        /// </summary>
        int Id { get; set; }

        [MaxLength(100)]
        [Required]
        string Name { get; set; }
        [MaxLength(255)]
        string Description { get; set; }

        /// <summary>
        /// one-to-many relationship between franchise and movie
        /// One movie belongs to one franchise, but a franchise can contain many movies
        /// </summary>
        ICollection<Movie> Movies { get; set; }
    }
}
