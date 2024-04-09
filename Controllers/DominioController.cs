using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

namespace APISipinnaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DominioController : ControllerBase
    {
        private readonly Conexiones _context;

        public DominioController(Conexiones context)
        {
            _context = context;
        }

        // GET: api/Dominio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dominio>>> GetdominioTbl()
        {
            return await _context.dominioTbl.ToListAsync();
        }

        // GET: api/Dominio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dominio>> GetDominio(int id)
        {
            var dominio = await _context.dominioTbl.FindAsync(id);

            if (dominio == null)
            {
                return NotFound();
            }

            return dominio;
        }

        // PUT: api/Dominio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDominio(int id, Dominio dominio)
        {
            if (id != dominio.iddominio)
            {
                return BadRequest();
            }

            _context.Entry(dominio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DominioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dominio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dominio>> PostDominio(Dominio dominio)
        {
            _context.dominioTbl.Add(dominio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDominio", new { id = dominio.iddominio }, dominio);
        }

        // DELETE: api/Dominio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDominio(int id)
        {
            var dominio = await _context.dominioTbl.FindAsync(id);
            if (dominio == null)
            {
                return NotFound();
            }

            _context.dominioTbl.Remove(dominio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DominioExists(int id)
        {
            return _context.dominioTbl.Any(e => e.iddominio == id);
        }
    }
}
