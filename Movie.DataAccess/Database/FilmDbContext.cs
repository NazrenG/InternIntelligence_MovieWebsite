using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DataAccess.Database
{
    public class FilmDbContext : IdentityDbContext<User, Role, string>
    {
        public FilmDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Film> Film { get; set; }   
    }
}
