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
        public async Task<IActionResult> CreateCelebrity([FromBody] PostCreateCelebrity input)
        {
            int status;
            string error = null;
            Celebrity celebrity = new Celebrity()
            {
                name = input.name,
                image = input.image,
                surname = input.surname,
                country = input.country,
                language = input.language,
                biography = input.biography,
                bornDate = input.bornDate,
                genres = input.genres
            };
            try
            {
                _context.Celebrities.Add(celebrity);
                await _context.SaveChangesAsync();
                status = HttpConstants.RESOURCE_CREATED;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                status = 500;
                error = "ERROR INTERNO. VERIFIQUE LOGS.";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpDelete]
        [Route("celebrities/{id}")]
        public ActionResult DeleteCelebrity(int id) {
            int status;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            if (exists)
            {
                try
                {
                    Celebrity celebrity = new Celebrity { id = id };
                    _context.Celebrities.Attach(celebrity);
                    _context.Celebrities.Remove(celebrity);
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
        [Route("celebrities/{id}")]
        public async Task<IActionResult> PutEditCelebrity([FromBody] PutEditCelebrity input, int id)
        { 
            int status;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            if (exists)
            {
                var celebrity = new Celebrity()
                {
                    id = id,
                    name = input.name,
                    image = input.image,
                    surname = input.surname,
                    country = input.country,
                    language = input.language,
                    biography = input.biography,
                    bornDate = input.bornDate,
                    genres = input.genres
                };
                try
                {
                    _context.Celebrities.Update(celebrity);
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
        [Route("celebrities")]
        public ActionResult GetCelebrities([FromQuery] GetFilters filters)
        {
            int limit = filters.limit;
            int offset = filters.offset;
            string orderBy = filters.orderBy;
            Celebrity[] celebrities;
            switch (orderBy)
            {
                case "title":
                    celebrities = _context.Celebrities.Take(limit).Skip(offset).OrderBy(celebrity => celebrity.surname).ToArray();
                    break;
                case "rating":
                    celebrities = _context.Celebrities.Take(limit).Skip(offset).OrderBy(celebrity => celebrity.rating).ToArray();
                    break;
                default:
                    celebrities = _context.Celebrities.Take(limit).Skip(offset).OrderBy(celebrity => celebrity.surname).ToArray();
                    break;
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", HttpConstants.SUCCESS_DATA);
            data.Add("error", null);
            data.Add("data", celebrities);
            return Json(data);
        }

        [HttpGet]
        [Route("celebrities/{id}")]
        public ActionResult GetCelebrity(int id)
        {
            int status;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                try
                {
                    Celebrity celebrity = _context.Celebrities.Where(celebrity => celebrity.id == id).FirstOrDefault();
                    data.Add("data", celebrity);
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
        [Route("celebrities/link/celebrities/{id}")]
        public async Task<IActionResult> LinkCelebritiesToCelebrity([FromBody] LinkCelebritiesToCelebrity input, int id){
            int status = 500;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idCelebrity in input.celebrityIds)
                {
                    bool existsLink = _context.LinkedCelebrityWithCelebrities.Any(celebrityLink => 
                    (celebrityLink.celebrityId1 == idCelebrity || celebrityLink.celebrityId2 == idCelebrity) &&
                    (celebrityLink.celebrityId1 == id || celebrityLink.celebrityId2 == id)
                    );
                    status = HttpConstants.RESOURCE_CREATED;
                    if (!existsLink)
                    {
                        try
                        {

                            var celebrity = new LinkedCelebrityWithCelebrity()
                            {
                                celebrityId1 = id,
                                celebrityId2 = idCelebrity
                            };
                            _context.LinkedCelebrityWithCelebrities.Add(celebrity);
                            await _context.SaveChangesAsync();
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            status = 500;
                            error = "ERROR INTERNO. VERIFIQUE LOGS.";
                        }
                    }
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
        [Route("celebrities/link/items/{id}")]
        public async Task<IActionResult> LinkItemsToCelebrity([FromBody] LinkItemsToCelebrity input, int id)
        {
            int status = 500;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idItem in input.itemIds)
                {
                    bool existsLink = _context.LinkedItemWithCelebrities.Any(celebrityLink =>
                        celebrityLink.itemId== idItem  && celebrityLink.celebrityId == id
                    );
                    status = HttpConstants.RESOURCE_CREATED;
                    if (!existsLink)
                    {
                        try
                        {

                            var celebrity = new LinkedItemWithCelebrity()
                            {
                                celebrityId = id,
                                itemId = idItem
                            };
                            _context.LinkedItemWithCelebrities.Add(celebrity);
                            await _context.SaveChangesAsync();
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            status = 500;
                            error = "ERROR INTERNO. VERIFIQUE LOGS.";
                        }
                    }
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
        [Route("celebrities/unlink/celebrity/{id}")]
        public ActionResult UnlinkCelebrityFromCelebrity([FromBody] UnlinkCelebrityFromCelebrity input, int id)
        {
            int status = 500;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idCelebrity in input.celebrityIds)
                {

                    LinkedCelebrityWithCelebrity linkedCelebrityWithCelebrity = _context.LinkedCelebrityWithCelebrities.Where(celebrityLink =>
                        (celebrityLink.celebrityId1 == idCelebrity || celebrityLink.celebrityId2 == idCelebrity) &&
                        (celebrityLink.celebrityId1 == id || celebrityLink.celebrityId2 == id)
                    ).FirstOrDefault();
                    status = HttpConstants.RESOURCE_CREATED;
                    if (linkedCelebrityWithCelebrity != null)
                    {
                        try
                        {
                            _context.LinkedCelebrityWithCelebrities.Attach(linkedCelebrityWithCelebrity);
                            _context.LinkedCelebrityWithCelebrities.Remove(linkedCelebrityWithCelebrity);
                            _context.SaveChanges();
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            status = 500;
                            error = "ERROR INTERNO. VERIFIQUE LOGS.";
                        }
                    }
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
        [Route("celebrities/unlink/item/{id}")]
        public ActionResult UnlinkItemFromCelebrity([FromBody] UnlinkItemFromCelebrity input, int id)
        {
            int status = 500;
            string error = null;
            bool exists = _context.Celebrities.Any(celebrity => celebrity.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idItem in input.itemIds)
                {
                    bool existsLink = _context.LinkedItemWithCelebrities.Any(celebrityLink =>
                        celebrityLink.itemId == idItem && celebrityLink.celebrityId == id
                    );
                    status = HttpConstants.RESOURCE_CREATED;
                    if (existsLink)
                    {
                        try
                        {

                            var celebrityLinkedWithItem = new LinkedItemWithCelebrity()
                            {
                                celebrityId = id,
                                itemId = idItem
                            };
                            _context.LinkedItemWithCelebrities.Attach(celebrityLinkedWithItem);
                            _context.LinkedItemWithCelebrities.Remove(celebrityLinkedWithItem);
                            _context.SaveChanges();
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            status = 500;
                            error = "ERROR INTERNO. VERIFIQUE LOGS.";
                        }
                    }
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
    }
}

