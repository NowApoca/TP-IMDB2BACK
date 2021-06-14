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
    public class ItemsController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public ItemsController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Value2/Index")]
        public async Task<IActionResult> Items()
        {
            Item[] array1 = new Item[50];
            Item newItem = new Item()
            {
                title = "adsasd",
                image = "./images/movie.jpg",
                userRating = 6.7,
                rating = 6.9,
                isBookmarked = false
            };
            array1.Append(newItem);
            array1.Append(newItem);
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("data", new[] {
                newItem,
                newItem
            });
            data.Add("status", 200);
            data.Add("error", null);
            return Json(data);
        }

        [HttpPost]
        [Route("items")]
        public ActionResult PostCreateItem([FromBody] PostCreateItem input) {

            return Json("probando json");
        }

        [HttpDelete]
        [Route("items/{title}")]
        public ActionResult DeleteItem(string title) {

            return Json("probando json");
        }

        [HttpPut]
        [Route("items/{title}")]
        public ActionResult EditItems([FromBody] PutEditUser input, string title) {

            return Json("probando json");
        }

        [HttpPatch]
        [Route("items/{title}")]
        public ActionResult PatchItemTitle([FromBody] PatchEditUser input, string title) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("items")]
        public ActionResult GetItems([FromBody] GetFilters filters) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("items/top/{topType}")]
        public ActionResult GetTopItems([FromBody] GetFilters filters, string topType) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("item/{title}")]
        public ActionResult GetItem(string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/link/celebrities/{title}")]
        public ActionResult LinkCelebritiesToItem([FromBody] LinkCelebritiesToItem input, string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/link/items/{title}")]
        public ActionResult LinkItemsToItem([FromBody] LinkItemsToItem input, string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/unlink/celebrity/{title}")]
        public ActionResult UnLinkCelebrityFromItem([FromBody] UnlinkCelebrityFromItem input, string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/unlink/item/{title}")]
        public ActionResult UnLinkItemFromItem([FromBody] UnlinkItemFromItem input, string title) {

            return Json("probando json");
        }


    }
}

