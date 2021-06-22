using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
    public class DeleteReactFromComment
    {
        public string reactionId {get; set;}
        public string commentId {get; set;}
    }
}
