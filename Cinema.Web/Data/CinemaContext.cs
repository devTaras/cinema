using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cinema.Web.Models;

namespace Cinema.Web.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext (DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public DbSet<Cinema.Web.Models.Producer> Producer { get; set; }

        public DbSet<Cinema.Web.Models.Movie> Movie { get; set; }

        public DbSet<Cinema.Web.Models.Actor> Actor { get; set; }
    }
}
