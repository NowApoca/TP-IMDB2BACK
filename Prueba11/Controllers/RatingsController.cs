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
    public class RatingsController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public RatingsController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("ratings")]
        public async Task<IActionResult> PostCreateRating([FromBody] PostCreateRating input, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (session != null && !session.expired)
            {
                if (input.type == "items")
                {
                    RatingItem ratingItemFromDb = _context.RatingItems.Where(
                        rating => rating.userName == session.userName &&
                        rating.itemId == input.entityId
                    ).FirstOrDefault();
                    if(ratingItemFromDb == null)
                    {
                        RatingItem ratingItem = new RatingItem()
                        {
                            userName = session.userName,
                            rating = input.rating,
                            itemId = input.entityId
                        };
                        try
                        {
                            _context.RatingItems.Add(ratingItem);
                            await _context.SaveChangesAsync();
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
                        ratingItemFromDb.rating = input.rating;
                        try
                        {
                            _context.RatingItems.Update(ratingItemFromDb);
                            await _context.SaveChangesAsync();
                            status = HttpConstants.SUCCESS_DATA;
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            status = 500;
                            error = "ERROR INTERNO. VERIFIQUE LOGS.";
                        }

                    }
                }
                else
                {
                    RatingCelebrity ratingCelebrityFromDb = _context.RatingCelebrities.Where(
                        rating => rating.userName == session.userName &&
                        rating.celebrityId == input.entityId
                    ).FirstOrDefault();
                    if (ratingCelebrityFromDb == null)
                    {
                        RatingCelebrity ratingItem = new RatingCelebrity()
                        {
                            userName = session.userName,
                            rating = input.rating,
                            celebrityId = input.entityId
                        };
                        try
                        {
                            _context.RatingCelebrities.Add(ratingItem);
                            await _context.SaveChangesAsync();
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
                        ratingCelebrityFromDb.rating = input.rating;
                        try
                        {
                            _context.RatingCelebrities.Update(ratingCelebrityFromDb);
                            await _context.SaveChangesAsync();
                            status = HttpConstants.SUCCESS_DATA;
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
                status = HttpConstants.BAD_REQUEST;
                error = "TOKEN INVALIDO.";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpGet]
        [Route("ratings/items")]
        public ActionResult GetUserRatings(GetFilters filters, [FromHeader] HeadersHelper headers) {
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            int limit = filters.limit;
            int offset = filters.offset;
            string orderBy = filters.orderBy;
            RatingItem[] ratingItems;
            ratingItems = _context.RatingItems.Where(ratingItem => ratingItem.userName == session.userName).Take(limit).Skip(offset).OrderBy(
                rating => rating.rating
            ).ToArray();
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", HttpConstants.SUCCESS_DATA);
            data.Add("error", null);
            data.Add("data", ratingItems);
            return Json(data);
        }
    }
}

