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

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Afghanistan",
                    AlphaCode2 = "AF",
                    AlphaCode3 = "AFG",
                    NumericCode = 004,
                    LinkSubdivision = "https://en.wikipedia.org/wiki/ISO_3166-2:AF",
                    Independent = true
                },
                new Country
                {
                    Id = 2,
                    Name = "Aland Islands",
                    AlphaCode2 = "AX",
                    AlphaCode3 = "ALA",
                    NumericCode = 248,
                    LinkSubdivision = "https://en.wikipedia.org/wiki/ISO_3166-2:AX",
                    Independent = false
                },
                new Country
                {
                    Id = 3,
                    Name = "Albania",
                    AlphaCode2 = "AL",
                    AlphaCode3 = "ALB",
                    NumericCode = 008,
                    LinkSubdivision = "https://en.wikipedia.org/wiki/ISO_3166-2:AL",
                    Independent = true
                }
                );

            modelBuilder.Entity<Subdivision>().HasData(
                new Subdivision
                {
                    Id = 1,
                    Name = "Badakhshān",
                    Code = "AF-BDS",
                    CountryId = 1
                },
                new Subdivision
                {
                    Id = 2,
                    Name = "Bādghīs",
                    Code = "AF-BDG",
                    CountryId = 1
                },
                new Subdivision
                {
                    Id = 3,
                    Name = "Kābul",
                    Code = "AF-KAB",
                    CountryId = 1
                },
                new Subdivision
                 {
                     Id = 4,
                     Name = "Berat",
                     Code = "AL-01",
                     CountryId = 3
                 },
                new Subdivision
                {
                    Id = 5,
                    Name = "Fier",
                    Code = "AL-04",
                    CountryId = 3
                }
                );

            modelBuilder.Entity<UserModel>().HasData(
               new UserModel
               {
                   Id = 1,
                   Email = "test@domain.com",
                   Password = "E99A18C428CB38D5F260853678922E03"
               });
        }
    }
}
