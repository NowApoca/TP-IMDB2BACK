using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class ReactionCommentCelebrity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id {get; set; }
        
        public CommentCelebrity CommentCelebrity { get; set;}
        public string reactionType {get; set; }
        public User User {get; set;}
    }
}
