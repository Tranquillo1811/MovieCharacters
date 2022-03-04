using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCharacters.BLL.Models
{
    public class CharacterAddDto
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string PictureUrl { get; set; }
    }
}
