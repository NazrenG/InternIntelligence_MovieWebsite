using Movie.Core.DataAccess;
using Movie.Entities.Models;

namespace Movie.DataAccess.Abstract
{
    public interface IUserRepository:IEntityRepository<User>
    {
    }
}
