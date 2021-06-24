using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class Celebrity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {get; set;}
        public string name {get; set; }
        public int rating{ get; set; }
        public string image {get; set;}
        public string surname {get; set;}
        public string country {get; set;}
        public string language {get; set;}
        public string biography {get; set;}
        public string bornDate {get; set;}
        public string genres {get; set;}
    }
}
