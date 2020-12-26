using Microsoft.EntityFrameworkCore;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cinema.Web.Data
{
    public class CinemaContext : IdentityDbContext
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
