using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public class Franchise
    {
        /// <summary>
        /// PrimaryKey
        /// </summary>
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// one-to-many relationship between franchise and movie
        /// One movie belongs to one franchise, but a franchise can contain many movies
        /// </summary>
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public ICollection<Movie>? Movies { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
