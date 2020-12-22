using Cinema.Web.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class Movie : ItemBase
    { 
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }


        public Genre Genre { get; set; }

        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]
        public string Rating { get; set; }

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public ICollection<Actor> Actors { get; set; }
    }

    public enum Genre
    {
        [Display(Name = "Комедія")]
        Comedy,
        [Display(Name = "Жахи")]
        Horor,
        [Display(Name = "Драма")]
        Drama
    }
}
