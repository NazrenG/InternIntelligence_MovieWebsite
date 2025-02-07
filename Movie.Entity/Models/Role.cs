using Microsoft.AspNetCore.Identity;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity.Models
{
    public class Role:IdentityRole,IEntity
    {
    }
}
