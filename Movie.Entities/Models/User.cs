using Microsoft.AspNetCore.Identity;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entities.Models
{
    public class User:IdentityUser,IEntity
    {
    }
}
