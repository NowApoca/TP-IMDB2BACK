using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba11.Context;
using Prueba11.Models;
using Prueba11.Helpers;

namespace Prueba11.Controllers
{
    public class RatingsController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public RatingsController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("users")]
        public ActionResult PostCreateRating([FromBody] PostCreateRating input) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("ratings")]
        public ActionResult GetUserRatings(GetFilters filters) {

            return Json("probando json");
        }

        [HttpPatch]
        [Route("users/{id}")]
        public ActionResult PatchEditRating([FromBody] PatchEditRating input) {

            return Json("probando json");
        }
    }
}

