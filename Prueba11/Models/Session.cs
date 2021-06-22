using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class Session
    {
        [Key]
        public string token {get; set;}
        public bool expired {get; set; }
        public User User { get; set; }
        public string issuedAt {get; set;}
    }
}
