using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba11.Models
{
    public class LinkedItemWithItem
    {
        public int itemId1 { get; set; }
        public int itemId2 { get; set; }
    }
}