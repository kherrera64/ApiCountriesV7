using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCountriesV7.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlphaCode2 { get; set; }
        public string AlphaCode3 { get; set; }
        public string LinkSubdivision { get; set; }
        public bool Independent { get; set; }
        public ICollection<Subdivision> Subdivisions { get; set; }
    }
}
