using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
        public class CommentWithReactions
        {
            public int id { get; set; }
            public string userName { get; set; }
            public string comment { get; set; }
            public bool isDeleted { get; set; }
            public int likes { get; set; }
            public int favourites { get; set; }
            public int smiles { get; set; }
            public int frownes { get; set; }
            public bool isLikedByUser { get; set; }
            public bool isFavouritedByUser { get; set; }
            public bool isSmiledByUser { get; set; }
            public bool isFrownedByUser { get; set; }
    }
}
