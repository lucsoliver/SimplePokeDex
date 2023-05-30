﻿using BlazorMovies.Base.Entidades;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CategoriaController (ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [EnableQuery()]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            try
            {

                var retorno = await context.Categorias.ToListAsync();
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.Message);
                return default;
            }
       
        }

        
        [HttpPost]
        public async Task<ActionResult<int>> Post(Categoria categoria)
        {
            
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();
            return categoria.Id;
        }
    }
}
