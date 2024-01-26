using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.ApplicationDbConteext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Movie.Movie> Movies { get; set; }
        
    }
}
