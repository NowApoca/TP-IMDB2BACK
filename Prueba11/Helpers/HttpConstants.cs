using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba11.Context;
using Prueba11.Models;
using Microsoft.Net.Http.Headers;


namespace Prueba11.Helpers
{
    public class HttpConstants
    {
        public static int SUCCESS_DATA = 200;
        public static int RESOURCE_CREATED = 201;
        public static int SUCCESS_NO_DATA = 204;

        public static int BAD_REQUEST = 400;
        public static int UNAUTHORIZED = 401;
        public static int FORBIDDEN = 403;
        public static int NOT_FOUND = 404;

        public static int INTERNAL_ERROR = 500;
    }
}
