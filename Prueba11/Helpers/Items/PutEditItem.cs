using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Helpers
{
    public class PutEditUser
    {
        public string image {get; set;}
        public string subTitle {get; set;}
        public int year {get; set;}
        public string summary {get; set;}
        public string director {get; set;}
        public string productor {get; set;}
        public string[] writers {get; set;}
        public string[] stars {get; set;}
        public string productorCountry {get; set;}
        public string language {get; set;}
        public string releaseDate {get; set;}
        public int duration {get; set;}
        public string genre {get; set;}
        public string budget {get; set;}
        public string earns {get; set;}
    }
}
