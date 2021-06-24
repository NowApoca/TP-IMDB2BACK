using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class User
    {
        [Key]
        public string userName {get; set;}
        public string password {get; set; }
        public bool isDeleted { get; set; }
        public string role {get; set;}
    }
}
