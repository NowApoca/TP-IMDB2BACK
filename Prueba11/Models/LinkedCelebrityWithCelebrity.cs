using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class LinkedCelebrityWithCelebrity
    {
        public int celebrityId1 { get; set; }
        public int celebrityId2 { get; set; }
    }
}
