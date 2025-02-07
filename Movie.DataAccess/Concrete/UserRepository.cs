using Movie.Core.DataAccess.EntityFramework;
using Movie.DataAccess.Abstract;
using Movie.Entities.Data;
using Movie.Entities.Models;

namespace Movie.DataAccess.Concrete
{
    public class UserRepository : EFEntityBaseRepository<MovieDbContext, User>,IUserRepository
    {
        public UserRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
