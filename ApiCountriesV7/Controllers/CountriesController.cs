using ApiCountriesV7.Data;
using ApiCountriesV7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCountriesV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public CountriesController(ApiDbContext dbContext)
        {
            // With this field now, we can access everything that's present in the API context class.
            _dbContext = dbContext;
        }

         /// <summary>
         /// return list of all countries.
         /// </summary>
         /// <returns></returns>
        // GET: api/<CountriesController>
        [HttpGet]
        //[Authorize]
        public IActionResult Get()
        {
            var countries = from country in _dbContext.Countries
                            select new
                            {
                                Id = country.Id,
                                Name = country.Name,
                                AlphaCode2 = country.AlphaCode2,
                                AlphaCode3 = country.AlphaCode3,
                                NumericCode = country.NumericCode,
                                LinkSubdivision = country.LinkSubdivision,
                                Independent = country.Independent
                            };

            return Ok(countries);
        }

        /// <summary>
        /// Get country by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<CountriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var country = _dbContext.Countries.Find(id);
            if (country == null)
            {
                return NotFound("No record found against this Id");
            }
            return Ok(country);
        }

        /// <summary>
        /// Create a new country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        // POST api/<CountriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Country country)
        {
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Update country.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="countryObj"></param>
        /// <returns></returns>
        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Country countryObj)
        {
            var country = _dbContext.Countries.Find(id);
            if (country == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                country.Name = countryObj.Name;
                country.AlphaCode2 = countryObj.AlphaCode2;
                country.AlphaCode3 = countryObj.AlphaCode3;
                country.NumericCode = countryObj.NumericCode;
                country.LinkSubdivision = countryObj.LinkSubdivision;
                country.Independent = countryObj.Independent;
                _dbContext.SaveChanges();
                return Ok("Record updated succesfully");
            }
        }

        /// <summary>
        /// Delete country by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var country = _dbContext.Countries.Find(id);
            if (country == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Countries.Remove(country);
                _dbContext.SaveChanges();
                return Ok("Record deleted succesfully");
            }
        }

        /// <summary>
        /// Get country detail by id with all subdivisions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult CountryDetail(int id)
        {
            var countryDetails = _dbContext.Countries.Where(a => a.Id == id).Include(a => a.Subdivisions);
            return Ok(countryDetails);
        }

        /// <summary>
        /// Search coyntry by name or alfaCode2 and pagination.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alfa2"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult SearchCountry(string name = null, string alfa2 = null, int? pageNumber = null, int? pageSize = null)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 100;
            var countries = from country in _dbContext.Countries
                            where (country.Name.StartsWith(name) || String.IsNullOrEmpty(name)) 
                            where (country.AlphaCode2.StartsWith(alfa2) || String.IsNullOrEmpty(alfa2))
                            orderby country.Name ascending
                            select new
                            {
                                Id = country.Id,
                                Name = country.Name,
                                AlphaCode2 = country.AlphaCode2,
                                AlphaCode3 = country.AlphaCode3,
                                NumericCode = country.NumericCode,
                                LinkSubdivision = country.LinkSubdivision,
                                Independent = country.Independent
                            };

            int countData = countries.Count();

            countries = countries.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize);

            var retunData = new { Page = currentPageNumber, TotalCountries = countData, Countries = countries };

            return Ok(retunData);
        }
    }
}
