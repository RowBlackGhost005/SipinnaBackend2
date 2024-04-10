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

        public readonly DominioDAO dominiodao;
        public DominioController(DominioDAO dominiodao)
        {
            this.dominiodao = dominiodao;
        }

        // GET: api/Dominio
        [HttpGet]
        public async Task<IEnumerable<Dominio>> GetdominioTbl()
        {
            return await dominiodao.getDominio();
        }

        // GET: api/Dominio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dominio>> GetDominio(int id)
        {
            var dominio = await dominiodao.getDominioId(id);

            return dominio;
        }

        // PUT: api/Dominio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<int> PutDominio(Dominio dominio){

            var dominioActualizado = await dominiodao.updateDominio(dominio);

            return dominioActualizado;
   
        }

        // POST: api/Dominio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dominio>> PostDominio(Dominio dominio)
        {
            var dominioCreado = await dominiodao.createDominio(dominio);

            return dominioCreado;
        }

        // DELETE: api/Dominio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDominio(int id)
        {
            await dominiodao.deleteDominio(id);

            return NoContent();
        }

    }
}
