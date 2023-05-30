﻿using BlazorMovies.Base.Entidades;
using BlazorMovies.Server.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        public LivroController(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Livro>>> Get()
        {    
            return  await context.Livros.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Livro livro)
        {
            if (!string.IsNullOrEmpty(livro.Poster))
            {
                var img = Convert.FromBase64String(livro.Poster);
                livro.Poster = await fileStorageService.SaveFile(img, "jpg", "livros");
            }

            if (livro.LivroPessoa != null)
            {
                for (int i = 0; i < livro.LivroPessoa.Count; i++)
                    livro.LivroPessoa[i].Order = i + 1;
            }

            context.Add(livro);
            await context.SaveChangesAsync();
            return livro.Id;
        }
    }
}
