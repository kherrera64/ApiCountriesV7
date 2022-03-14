using ApiCountriesV7.Data;
using ApiCountriesV7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCountriesV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdivisionsController : ControllerBase
    {

        private ApiDbContext _dbContext;

        public SubdivisionsController(ApiDbContext dbContext)
        {
            // With this field now, we can access everything that's present in the API context class.
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all subdivisions.
        /// </summary>
        /// <returns></returns>
        // GET: api/<SubdivisionsController>
        [HttpGet]
        public IActionResult Get()
        {
            var subdivision =_dbContext.Subdivisions;
            return Ok(subdivision);
        }

        /// <summary>
        /// Get subdivision by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<SubdivisionsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var subdivision = _dbContext.Subdivisions.Find(id);
            if (subdivision == null)
            {
                return NotFound("No record found against this Id");
            }
            return Ok(subdivision);
        }

        /// <summary>
        /// Create a new subdivision of a country.
        /// </summary>
        /// <param name="subdivision"></param>
        /// <returns></returns>
        // POST api/<SubdivisionsController>
        [HttpPost]
        public IActionResult Post([FromBody] Subdivision subdivision)
        {
            _dbContext.Subdivisions.Add(subdivision);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Update a subdivision of a country.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subdivisionObj"></param>
        /// <returns></returns>
        // PUT api/<SubdivisionsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Subdivision subdivisionObj)
        {
            var subdivision = _dbContext.Subdivisions.Find(id);
            if (subdivision == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                subdivision.Name = subdivisionObj.Name;
                subdivision.Code = subdivisionObj.Code;
                subdivision.CountryId = subdivisionObj.CountryId;
                _dbContext.SaveChanges();
                return Ok("Record updated succesfully");
            }
        }

        /// <summary>
        /// Delete subdivision by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<SubdivisionsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subdivision = _dbContext.Subdivisions.Find(id);
            if (subdivision == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Subdivisions.Remove(subdivision);
                _dbContext.SaveChanges();
                return Ok("Record deleted succesfully");
            }
        }
    }
}
