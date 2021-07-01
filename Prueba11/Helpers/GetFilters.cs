using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
    public class GetFilters
    {
        public int limit {get; set;}
        public int offset {get; set;}
        public string orderBy {get; set; }
        public string filter { get; set; }
    }
}
