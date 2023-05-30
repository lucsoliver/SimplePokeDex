﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Base.Entidades;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BlazorMovies.Server.Controllers
{

    public class CbmBaseController<TEntity>  : ControllerBase where TEntity : class, IEntity
    {

        protected ApplicationDbContext _context { get; set; }
        private readonly DbSet<TEntity> _dbSet;
     
        public CbmBaseController(DbContext context)
        {
            
      
            _dbSet = context.Set<TEntity>();
           
        }

        [HttpGet]
        [EnableQuery()]
        public async Task<ActionResult<List<TEntity>>> Get()
        {
            return await _dbSet.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(TEntity item )
        {
            _dbSet.Attach(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemToRemove = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (itemToRemove == null)
            {
                return NotFound();
            }

            _context.Remove(itemToRemove);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
