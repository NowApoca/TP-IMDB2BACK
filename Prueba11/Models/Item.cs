using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string title { get; set; }
        public string image { get; set; }
        public double userRating { get; set; }
        public double rating { get; set; }
        public bool isBookmarked { get; set; }
    }
}
