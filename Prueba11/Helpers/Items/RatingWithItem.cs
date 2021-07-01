using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
    public class RatingWithItem
    {
        public string image {get; set;}
        public int id {get; set;}
        public int userRating {get; set;}
        public double rating {get; set;}
        public bool isBookmarked {get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
    }
}
