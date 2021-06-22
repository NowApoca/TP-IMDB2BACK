using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class CommentItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id {get; set; }
        public CommentItem ParentCommentItem{ get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
        public string comment {get; set;}
        public bool isDeleted {get; set;}
    }
}
