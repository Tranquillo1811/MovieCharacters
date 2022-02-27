using System.Collections.Generic;

namespace MovieCharacters.BLL.Models
{
    public interface IMovie
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
