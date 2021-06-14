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
    public class CelebritiesController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public CelebritiesController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("celebrities")]
        public ActionResult CreateCelebrity([FromBody] PostCreateCelebrity input) {

            return Json("probando json");
        }

        [HttpDelete]
        [Route("celebrities/{title}")]
        public ActionResult DeleteCelebrity(string title) {

            return Json("probando json");
        }

        [HttpPut]
        [Route("celebrities/{title}")]
        public ActionResult EditCelebrities([FromBody] PutEditCelebrity input, string title) {

            return Json("probando json");
        }

        [HttpPatch]
        [Route("celebrities/{title}")]
        public ActionResult PatchCelebrityTitle([FromBody] PatchCelebrityTitle input, string title) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("celebrities")]
        public ActionResult GetCelebrities([FromBody] GetFilters filters) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("celebrities/top/{topType}")]
        public ActionResult GetTopItems([FromBody] GetFilters filters, string topType) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("item/{title}")]
        public ActionResult GetItem(string title) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("celebrities/link/celebrities/{title}")]
        public ActionResult LinkCelebritiesToCelebrity([FromBody] LinkCelebritiesToCelebrity input, string title) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("celebrities/link/items/{title}")]
        public ActionResult LinkItemsToCelebrity([FromBody] LinkItemsToCelebrity input, string title) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("celebrities/unlink/celebrity/{title}")]
        public ActionResult UnlinkCelebrityFromCelebrity([FromBody] UnlinkCelebrityFromCelebrity input, string title) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("celebrities/unlink/item/{title}")]
        public ActionResult UnlinkItemFromCelebrity([FromBody] UnlinkItemFromCelebrity input, string title) {

            return Json("probando json");
        }
    }
}

