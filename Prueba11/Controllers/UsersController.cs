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
    public class UsersController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public UsersController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("users")]
        public ActionResult PostCreateUser([FromBody] PostCreateUser input) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("users/administrator")]
        public ActionResult PostCreateAdministrator([FromBody] PostCreateAdministrator input) {

            return Json("probando json");
        }

        [HttpDelete]
        [Route("users/{email}")]
        public ActionResult DeleteUser(string email) {

            return Json("probando json");
        }

        [HttpPatch]
        [Route("users/password")]
        public ActionResult PatchUserPassword([FromBody] PatchUserPassword input) {

            return Json("probando json");
        }

        [HttpPatch]
        [Route("users/{email}")]
        public ActionResult PatchEditUserEmail([FromBody] PatchEditUserEmail input, string email) {

            return Json("probando json");
        }

        [HttpGet]
        [Route("users")]
        public ActionResult GetUsers([FromBody] GetFilters filters) {

            return Json("probando json");
        }
        
        [HttpGet]
        [Route("sessions")]
        public ActionResult GetSessions([FromBody] GetSessions input, string email) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("login")]
        public ActionResult PostLogin([FromBody] PostLogin input, string email) {

            return Json("probando json");
        }

        [HttpPost]
        [Route("logout")]
        public ActionResult PostLogout() {

            return Json("probando json");
        }

    }
}

