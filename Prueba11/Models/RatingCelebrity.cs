using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class RatingCelebrity
    {
        public string userName { get; set; }
        public int celebrityId { get; set; }
        public int rating {get; set;}
    }
}
