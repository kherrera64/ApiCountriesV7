using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCountriesV7.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        public string AlphaCode2 { get; set; }
        public string AlphaCode3 { get; set; }
        public int NumericCode { get; set; }
        public string LinkSubdivision { get; set; }
        public bool Independent { get; set; }
        public ICollection<Subdivision> Subdivisions { get; set; }
    }
}
