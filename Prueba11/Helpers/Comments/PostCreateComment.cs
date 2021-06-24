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
        public string type {get; set;}
        public int entityId {get; set;}
        public int parentCommentId { get; set; }
    }
}
