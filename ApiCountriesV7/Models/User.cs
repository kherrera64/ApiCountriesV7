using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCountriesV7.Models
{
    public class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
