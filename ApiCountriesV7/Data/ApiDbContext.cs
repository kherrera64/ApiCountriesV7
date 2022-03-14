using ApiCountriesV7.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCountriesV7.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<Subdivision> Subdivisions { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
