using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public class MovieUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string MoviePosterUrl { get; set; }
        public string TrailerUrl { get; set; }
    }
}
