﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    public class ClienteCategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
