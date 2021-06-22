using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
    public class PostCreateAdministrator
    {
        public string username {get; set;}
        public string password {get; set;}
        public string role {get; set;}
    }
}
