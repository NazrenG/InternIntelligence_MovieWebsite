using Movie.Core.DataAccess;
using Movie.Entities.Models;

namespace Movie.DataAccess.Abstract
{
    public interface IMovieRepository:IEntityRepository<Film>
    {
    }
}
