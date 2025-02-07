using Movie.Business.Abstract;
using Movie.DataAccess.Abstract;
using Movie.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetById(p => p.Id == id);
        }
    }
}
