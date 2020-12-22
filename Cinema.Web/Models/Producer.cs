using Cinema.Web.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Web.Models
{
    public class Producer : ItemBase
    {
        /// <summary>
        /// Ім'я
        /// </summary>
        [Required]
        [MinLength(3)]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Електронна пошта
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// День народження
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// К-сть фільмів
        /// </summary>
        [Range(1, 100)]
        public int CountOfFilms { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Salary { get; set;}

        [Column(TypeName = "decimal(18,4)")]
        public decimal Rating { get; set; }

        [MaxLength(255)]
        public string  Description { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
