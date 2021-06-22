using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba11.Context;
using Prueba11.Models;
using Prueba11.Helpers;
using Microsoft.Net.Http.Headers;

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
                image = "./images/movie.jpg",
                rating = 6
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
        public async Task<IActionResult> PostCreateItem([FromBody] PostCreateItem input)
        {
            int status;
            string error = null;
            bool exists = _context.Items.Any(item => item.title == input.title);
            if (!exists)
            {
                var item = new Item()
                {
                    title = input.title,
                    image = input.image,
                    subtitle = input.subtitle,
                    year = input.year,
                    summary = input.summary,
                    director = input.director,
                    productor = input.productor,
                    writers = input.writers,
                    stars = input.stars,
                    productorCountry = input.productorCountry,
                    language = input.language,
                    releaseDate = input.releaseDate,
                    duration = input.duration,
                    genre = input.genre,
                    budget = input.budget,
                    earns = input.earns,
                };
                try
                {
                    _context.Items.Add(item);
                    await _context.SaveChangesAsync();
                    status = HttpConstants.RESOURCE_CREATED;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    status = 500;
                    error = "ERROR INTERNO. VERIFIQUE LOGS.";
                }
            }
            else
            {
                status = HttpConstants.BAD_REQUEST;
                error = "EL ITEM YA EXISTE";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpDelete]
        [Route("items/{title?}")]
        public ActionResult DeleteItem(string title, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            bool exists = _context.Items.Any(item => item.title == title);
            if (exists)
            {
                try
                {
                    Item item = new Item { title = title };
                    _context.Items.Attach(item);
                    _context.Items.Remove(item);
                    _context.SaveChanges();
                    status = HttpConstants.SUCCESS_NO_DATA;
                }
                catch(IOException e)
                {
                    Console.WriteLine(e.Message);
                    status = 500;
                    error = "ERROR INTERNO. VERIFIQUE LOGS.";
                }
            }
            else
            {
                status = HttpConstants.NOT_FOUND;
                error = "NO SE HA ENCONTRADO EL ITEM.";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpPut]
        [Route("items/{id?}")]
        public async Task<IActionResult> PutEditItem(int id, [FromBody] PutEditItem input)
        {
            int status;
            string error = null;
            bool exists = _context.Items.Any(item => item.id == id);
            if (exists)
            {
                var item = new Item()
                {
                    title = input.title,
                    image = input.image,
                    subtitle = input.subtitle,
                    year = input.year,
                    summary = input.summary,
                    director = input.director,
                    productor = input.productor,
                    writers = input.writers,
                    stars = input.stars,
                    productorCountry = input.productorCountry,
                    language = input.language,
                    releaseDate = input.releaseDate,
                    duration = input.duration,
                    genre = input.genre,
                    budget = input.budget,
                    earns = input.earns,
                };
                try
                {
                    _context.Items.Update(item);
                    await _context.SaveChangesAsync();
                    status = HttpConstants.SUCCESS_NO_DATA;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    status = 500;
                    error = "ERROR INTERNO. VERIFIQUE LOGS.";
                }
            }
            else
            {
                status = HttpConstants.NOT_FOUND;
                error = "NO SE HA ENCONTRADO EL ITEM.";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpGet]
        [Route("items")]
        public ActionResult GetItems([FromQuery] GetFilters filters) {
            int limit = filters.limit;
            int offset = filters.offset;
            string orderBy = filters.orderBy;
            Item[] items;
            switch (orderBy)
            {
                case "title":
                    items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.title).ToArray();
                    break;
                case "rating":
                    items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.rating).ToArray();
                    break;
                default:
                    items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.title).ToArray();
                    break;
            }
            Console.WriteLine(items.Length + "items.Length");
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", HttpConstants.SUCCESS_DATA);
            data.Add("error", null);
            data.Add("data", items);
            return Json(data);
        }

        [HttpGet]
        [Route("items/top/{topUserType?}")]
        public ActionResult GetTopItems([FromQuery] GetFilters filters, string topUserType)
        {
            int limit = filters.limit;
            int offset = filters.offset;
            Item[] items;
            Celebrity[] celebrities;
            IDictionary<string, object> data = new Dictionary<string, object>();
            switch (topUserType) 
            {
                case "topUserCelebrities":
                    celebrities = _context.Celebrities.Take(limit).Skip(offset).OrderBy(item => item.surname).ToArray();
                    data.Add("data", celebrities);
                    break;
                case "topUserItems":
                    items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.title).ToArray();
                    data.Add("data", items);
                    break;
                default:
                    items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.title).ToArray();
                    data.Add("data", items);
                    break;
            }
            data.Add("status", HttpConstants.SUCCESS_DATA);
            data.Add("error", null);
            return Json(data);
        }

        [HttpGet]
        [Route("items/{title?}")]
        public ActionResult GetItem(string title)
        {
            int status;
            string error = null;
            bool exists = _context.Items.Any(item => item.title == title);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                try
                {
                    Item item = _context.Items.Where(item => item.title == title).FirstOrDefault();
                    data.Add("data", item);
                    status = HttpConstants.SUCCESS_DATA;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    status = 500;
                    error = "ERROR INTERNO. VERIFIQUE LOGS.";
                }
            }
            else
            {
                status = HttpConstants.NOT_FOUND;
                error = "NO SE HA ENCONTRADO EL ITEM.";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }



        [HttpPost]
        [Route("items/link/celebrities/{title?}")]
        public ActionResult LinkCelebritiesToItem([FromBody] LinkCelebritiesToItem input, string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/link/items/{title?}")]
        public ActionResult LinkItemsToItem([FromBody] LinkItemsToItem input, string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/unlink/celebrity/{title?}")]
        public ActionResult UnLinkCelebrityFromItem([FromBody] UnlinkCelebrityFromItem input, string title) {

            return Json("probando json");
        }



        [HttpPost]
        [Route("items/unlink/item/{title?}")]
        public ActionResult UnLinkItemFromItem([FromBody] UnlinkItemFromItem input, string title) {

            return Json("probando json");
        }


    }
}
