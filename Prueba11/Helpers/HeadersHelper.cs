using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba11.Context;
using Prueba11.Models;
using Microsoft.Net.Http.Headers;


namespace Prueba11.Helpers
{
    public class HeadersHelper
    {
        [FromHeader]
        public string Header1 { get; set; }

        [FromHeader]
        public string Header2 { get; set; }
        [FromHeader]
        public string Authorization { get; set; }
    }
}
