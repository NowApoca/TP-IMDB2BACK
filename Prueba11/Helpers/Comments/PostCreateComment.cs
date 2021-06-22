using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
    public class PostCreateComment
    {
        public string comment {get; set;}
        public int type {get; set;}
        public string entityId {get; set;}
    }
}
