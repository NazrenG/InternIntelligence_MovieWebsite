using Movie.Core.DataAccess.EntityFramework;
using Movie.DataAccess.Abstract;
using Movie.Entities.Data;
using Movie.Entities.Models;

namespace Movie.DataAccess.Concrete
{
    public class MovieRepository : EFEntityBaseRepository<MovieDbContext, Film>, IMovieRepository
    {
        public MovieRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
