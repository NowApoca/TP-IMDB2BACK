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
    public class UsersController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public UsersController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> PostCreateUser([FromBody] PostCreateUser input)
        {
            int status;
            string error = null;
            bool exists = _context.Users.Any(user => user.userName == input.userName);
            if (!exists)
            {
                var user = new User()
                {
                    userName = input.userName,
                    password = input.password,
                    isDeleted = false,
                    role = "USUARIO"
                };
                try
                {
                    _context.Users.Add(user);
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
                error = "EL USUARIO YA EXISTE";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpPost]
        [Route("users/administrator")]
        public async Task<IActionResult> PostCreateAdministrator([FromBody] PostCreateAdministrator input)
        {
            int status;
            string error = null;
            bool exists = _context.Users.Any(user => user.userName == input.userName);
            if (!exists)
            {
                var user = new User()
                {
                    userName = input.userName,
                    password = input.password,
                    isDeleted = false,
                    role = input.role
                };
                try
                {
                    _context.Users.Add(user);
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
                error = "EL USUARIO YA EXISTE";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpDelete]
        [Route("users/{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            int status;
            string error = null;
            User user = _context.Users.Where(user => user.userName == userName).FirstOrDefault();
            if (user != null)
            {
                user.isDeleted = true;
                try
                {
                    _context.Users.Update(user);
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

        [HttpPatch]
        [Route("users/password")]
        public async Task<IActionResult> PatchUserPassword([FromBody] PatchUserPassword input, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (session != null && !session.expired)
            {
                User user = _context.Users.Where(user => user.userName == session.userName).FirstOrDefault();
                if (input.oldPassword.Equals(user.password))
                {
                    user.password = input.newPassword;
                    try
                    {
                        _context.Users.Update(user);
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
                    status = HttpConstants.BAD_REQUEST;
                    error = "PASSWORD INVALIDA.";
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
        [Route("users")]
        public ActionResult GetUsers([FromQuery] GetFilters filters)
        {
            int limit = filters.limit;
            int offset = filters.offset;
            User[] users;
            users = _context.Users.Take(limit).Skip(offset).OrderBy(user => user.userName).ToArray();
            Console.WriteLine(users.Length + "items.Length");
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", HttpConstants.SUCCESS_DATA);
            data.Add("error", null);
            data.Add("data", users);
            return Json(data);
        }
        
        [HttpGet]
        [Route("sessions")]
        public ActionResult GetSessions([FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (session != null && !session.expired)
            {
                User user = _context.Users.Where(user=> user.userName == session.userName).FirstOrDefault();
                data.Add("data", user);
                status = HttpConstants.SUCCESS_DATA;
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> PostLogin([FromBody] PostLogin input)
        {
            int status;
            string error = null;
            IDictionary<string, object> data = new Dictionary<string, object>();
            User user = _context.Users.Where(item => item.userName == input.userName).FirstOrDefault();
            if (user != null)
            {
                if (user.password.Equals(input.password) && !user.isDeleted)
                {
                    Session session = new Session
                    {
                        expired = false,
                        userName = user.userName
                    };
                    try
                    {
                        _context.Sessions.Add(session);
                        await _context.SaveChangesAsync();
                        status = HttpConstants.SUCCESS_DATA;
                        data.Add("data", session.token);
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
                    error = "EL USUARIO O LA PASSWORD ES INVALIDA";
                }
            }
            else
            {
                status = HttpConstants.BAD_REQUEST;
                error = "EL USUARIO O LA PASSWORD ES INVALIDA";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> PostLogout([FromHeader] HeadersHelper headers) {
            int status;
            string error = null;
            IDictionary<string, object> data = new Dictionary<string, object>();
            Session session = _context.Sessions.Where(session=> session.token== headers.Authorization).FirstOrDefault();
            if (session != null)
            {
                session.expired= true;
                try
                {
                    _context.Sessions.Update(session);
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
                status = HttpConstants.BAD_REQUEST;
                error = "LA SESION ES INVALIDA";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }




        [HttpPost]
        [Route("users/watchlist")]
        public async Task<IActionResult> PostUserWatchlist([FromBody] PostUserWatchlist input, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (session != null && !session.expired)
            {
                Item item = _context.Items.Where(item => item.id== input.itemId).FirstOrDefault();
                if(item != null)
                {
                    UserWatchlist watchlist = _context.UserWatchlists.Where(watchlist => watchlist.itemId == input.itemId
                    && watchlist.userName == session.userName).FirstOrDefault();
                    if (watchlist == null)
                    {
                        UserWatchlist newWatchList = new UserWatchlist()
                        {
                            userName = session.userName,
                            itemId = input.itemId
                        };
                        try
                        {
                            _context.UserWatchlists.Add(newWatchList);
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

                        try
                        {
                            _context.UserWatchlists.Attach(watchlist);
                            _context.UserWatchlists.Remove(watchlist);
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
                }
                else
                {
                    status = HttpConstants.BAD_REQUEST;
                    error = "EL ITEM NO EXISTE.";
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


    }
}

