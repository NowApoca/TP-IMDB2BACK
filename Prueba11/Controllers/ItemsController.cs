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
        [Route("items/{id?}")]
        public ActionResult DeleteItem(int id, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            bool exists = _context.Items.Any(item => item.id == id);
            if (exists)
            {

                bool existsCelebrityRelations = _context.LinkedItemWithCelebrities.Any(item => item.itemId == id);
                bool existsItemRelations = _context.LinkedItemWithItems.Any(item => item.itemId1 == id || item.itemId2 == id);
                if (!existsCelebrityRelations && !existsItemRelations)
                {
                    try
                    {
                        Item item = new Item { id = id };
                        _context.Items.Attach(item);
                        _context.Items.Remove(item);
                        _context.SaveChanges();
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
                    status = HttpConstants.BAD_REQUEST;
                    error = "EL ITEM AUN TIENE RELACIONES";
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
                    id = id,
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
        public ActionResult GetItems([FromQuery] GetFilters filters, [FromHeader] HeadersHelper headers) {
            int limit = filters.limit;
            int offset = filters.offset;
            string orderBy = filters.orderBy;
            string filter = filters.filter;
            Item[] items;
            switch (orderBy)
            {
                case "id":
                    if (filter == null)
                    {
                        items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.id).ToArray();
                    }
                    else
                    {
                        items = _context.Items.Take(limit).Skip(offset).Where(item => EF.Functions.Like(item.title, filter)).OrderBy(item => item.id).ToArray();
                    }
                    break;
                case "title":
                    if (filter == null)
                    {
                        items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.title).ToArray();
                    }
                    else
                    {
                        items = _context.Items.Take(limit).Skip(offset).Where(item => EF.Functions.Like(item.title, filter)).OrderBy(item => item.title).ToArray();
                    }
                    break;
                default:
                    if (filter == null)
                    {
                        items = _context.Items.Take(limit).Skip(offset).OrderBy(item => item.id).ToArray();
                    }
                    else
                    {
                        items = _context.Items.Take(limit).Skip(offset).Where(item => EF.Functions.Like(item.title, filter)).OrderBy(item => item.id).ToArray();
                    }
                    break;
            }
            Session session;
            IDictionary<string, object> data = new Dictionary<string, object>();
            RatingWithItem[] itemsGet = new RatingWithItem[limit];
            int currentIndex = 0;
            foreach (Item element in items)
            {
                RatingWithItem ratingWithRatingAndBookmark = new RatingWithItem()
                {
                    id = element.id,
                    image = element.image,
                    title = element.title,
                    subtitle = element.subtitle
                };

                double totalRating = _context.RatingItems.Where(ratingItem => ratingItem.itemId == element.id).Sum(r => r.rating);
                int totalRatingCount = _context.RatingItems.Where(ratingItem => ratingItem.itemId == element.id).Count();

                if (totalRatingCount != 0)
                {
                    ratingWithRatingAndBookmark.rating = totalRating / totalRatingCount;
                }
                else
                {
                    ratingWithRatingAndBookmark.rating = 0;
                }
                if (headers.Authorization != null)
                {
                    session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
                    if (session != null)
                    {
                        RatingItem rating = _context.RatingItems.Where(ratingItem => ratingItem.userName == session.userName && ratingItem.itemId == element.id).FirstOrDefault();
                        UserWatchlist userWatchList = _context.UserWatchlists.Where(watchlist => watchlist.userName == session.userName && watchlist.itemId == element.id).FirstOrDefault();
                        if (rating != null)
                        {
                            ratingWithRatingAndBookmark.userRating = rating.rating;
                        }
                        if (userWatchList != null)
                        {
                            ratingWithRatingAndBookmark.isBookmarked = true;
                        }
                        else
                        {
                            ratingWithRatingAndBookmark.isBookmarked = false;
                        }
                    }
                }
                itemsGet[currentIndex] = ratingWithRatingAndBookmark;
                currentIndex++;
            }
            data.Add("data", itemsGet);
            data.Add("status", HttpConstants.SUCCESS_DATA);
            data.Add("error", null);
            return Json(data);
        }

        [HttpGet]
        [Route("items/{id?}")]
        public ActionResult GetItem(int id, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            bool exists = _context.Items.Any(item => item.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                try
                {
                    Item item = _context.Items.Where(item => item.id == id).FirstOrDefault();

                    double totalRating = _context.RatingItems.Where(ratingItem => ratingItem.itemId == id).Sum(r => r.rating);
                    int totalRatingCount = _context.RatingItems.Where(ratingItem => ratingItem.itemId == id).Count();
                    item.rating = totalRating / totalRatingCount;
                    var castFromDB = _context.Celebrities.Join(
                    _context.LinkedItemWithCelebrities,
                    entryPoint => entryPoint.id,
                    entry => entry.celebrityId,
                    (entryPoint, entry) => new { celebrity = entryPoint, relation = entry }).Where(itemCele => itemCele.relation.itemId == id).ToArray();
                    IDictionary<string, object> itemData = new Dictionary<string, object>();
                    var relatedMovies = _context.Items.Join(
                    _context.LinkedItemWithItems,
                    entryPoint => entryPoint.id,
                    entry => entry.itemId2,
                    (entryPoint, entry) => new { item = entryPoint, relation = entry }).Where(item => item.relation.itemId1 == id || item.relation.itemId1 == id).ToArray();
                    CommentItem[] comments = _context.CommentItems.Where(comment => comment.Item.id == id).ToArray();
                    Session session = null;
                    if (headers.Authorization != null)
                    {
                        session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
                    }
                    CommentWithReactions[] commentWithReactions = GetCommentsWithReactions.get(_context, comments, session);
                    itemData.Add("data", item);
                    itemData.Add("cast", castFromDB);
                    itemData.Add("relatedMovies", relatedMovies);
                    itemData.Add("comments", commentWithReactions);
                    data.Add("data", itemData);
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
        [Route("items/link/celebrities/{id?}")]
        public async Task<IActionResult> LinkCelebritiesToItem([FromBody] LinkCelebritiesToItem input, int id)
        {
            int status = 500;
            string error = null;
            bool itemExists = _context.Items.Any(item => item.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (itemExists)
            {
                foreach (int idCelebrity in input.celebrityIds)
                {
                    bool existsLink = _context.LinkedItemWithCelebrities.Any(celebrityLink =>
                        celebrityLink.celebrityId == idCelebrity && celebrityLink.itemId == id
                    );
                    status = HttpConstants.RESOURCE_CREATED;
                    if (!existsLink)
                    {
                        try
                        {

                            var item = new LinkedItemWithCelebrity()
                            {
                                celebrityId = idCelebrity,
                                itemId = id
                            };
                            _context.LinkedItemWithCelebrities.Add(item);
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
        [Route("items/link/items/{id?}")]
        public async Task<IActionResult> LinkItemsToItem([FromBody] LinkItemsToItem input, int id)
        {
            int status = 500;
            string error = null;
            bool exists = _context.Items.Any(item => item.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idItem in input.itemIds)
                {
                    bool existsLink = _context.LinkedItemWithItems.Any(itemLink =>
                    (itemLink.itemId1 == idItem || itemLink.itemId2 == idItem) &&
                    (itemLink.itemId1 == id || itemLink.itemId2 == id)
                    );
                    status = HttpConstants.RESOURCE_CREATED;
                    if (!existsLink)
                    {
                        try
                        {
                            var itemWithItem = new LinkedItemWithItem()
                            {
                                itemId1 = id,
                                itemId2 = idItem
                            };
                            _context.LinkedItemWithItems.Add(itemWithItem);
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
        [Route("items/unlink/celebrity/{id?}")]
        public ActionResult UnLinkCelebrityFromItem([FromBody] UnlinkCelebrityFromItem input, int id)
        {
            int status = 500;
            string error = null;
            bool exists = _context.Items.Any(item => item.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idCelebrity in input.celebrityIds)
                {

                    LinkedItemWithCelebrity linkedItemWithCelebrity = _context.LinkedItemWithCelebrities.Where(celebrityItemLink =>
                        celebrityItemLink.celebrityId == idCelebrity  && celebrityItemLink.itemId == id
                    ).FirstOrDefault();
                    Console.WriteLine(linkedItemWithCelebrity);
                    Console.WriteLine(linkedItemWithCelebrity.celebrityId);
                    status = HttpConstants.RESOURCE_CREATED;
                    if (linkedItemWithCelebrity != null)
                    {
                        try
                        {
                            _context.LinkedItemWithCelebrities.Attach(linkedItemWithCelebrity);
                            _context.LinkedItemWithCelebrities.Remove(linkedItemWithCelebrity);
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
        [Route("items/unlink/item/{id?}")]
        public ActionResult UnLinkItemFromItem([FromBody] UnlinkItemFromItem input, int id)
        {
            int status = 500;
            string error = null;
            bool exists = _context.Items.Any(item => item.id == id);
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (exists)
            {
                foreach (int idItem in input.itemIds)
                {

                    LinkedItemWithItem linkedItemWithItem = _context.LinkedItemWithItems.Where(itemWithItemLink =>
                        (itemWithItemLink.itemId1 == idItem || itemWithItemLink.itemId2 == idItem) &&
                        (itemWithItemLink.itemId1 == id || itemWithItemLink.itemId2 == id)
                    ).FirstOrDefault();
                    status = HttpConstants.RESOURCE_CREATED;
                    if (linkedItemWithItem != null)
                    {
                        try
                        {
                            _context.LinkedItemWithItems.Attach(linkedItemWithItem);
                            _context.LinkedItemWithItems.Remove(linkedItemWithItem);
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
