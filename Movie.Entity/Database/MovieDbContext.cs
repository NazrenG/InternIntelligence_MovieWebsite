using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity.Database
{
    public class MovieDbContext : IdentityDbContext<User, Role, string>
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Film> Movies { get; set; }
    }
}
