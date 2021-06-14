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
    public class CommentsController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public CommentsController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("comments")]
        public ActionResult LinkCelebritiesToCelebrity([FromBody] PostCreateComment input) {

            return Json("probando json");
        }

        [HttpDelete]
        [Route("comments/{id}")]
        public ActionResult DeleteDeleteComment([FromBody] DeleteDeleteComment input, string id) {

            return Json("probando json");
        }

        [HttpPatch]
        [Route("comments/{id}")]
        public ActionResult PatchEditComment([FromBody] PatchEditComment input, string id) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("comments/react/{id}")]
        public ActionResult AddReactToComment([FromBody] AddReactToComment input, int id) {

            return Json("probando json");
        }

        [HttpDelete]
        [Route("comments/react/{id}")]
        public ActionResult DeleteReactFromComment([FromBody] DeleteReactFromComment input, int id) {

            return Json("probando json");
        }
    }
}

