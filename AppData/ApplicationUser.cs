using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FavoriteCars = new List<FavoriteCar>();
        }
        public ICollection<FavoriteCar> FavoriteCars { get; set; }
    }
}
