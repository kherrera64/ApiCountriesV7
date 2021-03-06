using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCountriesV7.Models
{
    public class Subdivision
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
    }
}
