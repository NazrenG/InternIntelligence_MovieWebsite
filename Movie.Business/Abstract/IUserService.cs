using Movie.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Business.Abstract
{
    public interface IUserService
    {
        public Task<User> GetUserById (string id); 
    }
}
