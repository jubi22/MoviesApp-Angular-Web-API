using MoviesApi.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Movies_Actors
    {
        [Key]
        public int ID { get; set; }
        public int MovieID { get; set; }
        public string UserID { get; set; }

        [ForeignKey("MovieID")]
        public virtual Movies Movies { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
