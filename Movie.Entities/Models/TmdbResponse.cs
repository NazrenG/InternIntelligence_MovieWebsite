using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entities.Models
{
    public class TmdbResponse
    {
        public int Page { get; set; }
        public List<Film>? Results { get; set; }
    }
}
