using Cinema.Web.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class Actor : ItemBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
