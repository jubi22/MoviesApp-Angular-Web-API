using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Movies
    {
        [Key]
        public int MovieID { get; set; }
        [MaxLength(250)]
        public string MovieName { get; set; }
        public string Description { get; set; }
    }
}
