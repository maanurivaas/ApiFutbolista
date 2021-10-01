using ApiFutbolista.Context;
using ApiFutbolista.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiFutbolista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutbolistaController : ControllerBase
    {
        private readonly AppDbContext context;
        public FutbolistaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<FutbolistaController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.futbolistas.ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<FutbolistaController>/5
        [HttpGet("{id}",Name ="GetFutbolista")]
        public ActionResult Get(int id)
        {
            try
            {
                var futbolista = context.futbolistas.FirstOrDefault(f=>f.id==id);
                return Ok(futbolista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<FutbolistaController>
        [HttpPost]
        public ActionResult Post([FromBody] Futbolista futbolista)
        {
            try
            {
                context.futbolistas.Add(futbolista);
                context.SaveChanges();
                return CreatedAtRoute("GetFutbolista",new { id=futbolista.id}, futbolista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<FutbolistaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Futbolista futbolista)
        {
            try
            {
                if (futbolista.id==id)
                {
                    context.Entry(futbolista).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetFutbolista",new { id=futbolista.id},futbolista);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<FutbolistaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var futbolista = context.futbolistas.FirstOrDefault(f => f.id == id);
                if(futbolista!= null)
                {
                    context.futbolistas.Remove(futbolista);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
