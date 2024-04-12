using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SipinnaBackend2.Models;

namespace APISipinnaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticiasController : ControllerBase
    {
        public readonly NoticiasDAO noticias;

        public NoticiasController(NoticiasDAO noticias)
        {
            this.noticias = noticias;
        }

        //GET: api/Noticias
        [HttpGet]
        public async Task<IEnumerable<Noticias>> GetNoticias()
        {
            return await noticias.GetNoticias();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Noticias>> GetNoticia(int id)
        {
            return await noticias.GetNoticiasId(id);
        }

        [HttpPost]
        public async Task<ActionResult<Noticias>> PostNoticia(Noticias noticia)
        {
            return await noticias.CreateNoticias(noticia);
        }

        [HttpPut]
        public async Task<ActionResult<Noticias>> PutNoticia(Noticias noticia)
        {
            return await noticias.UpdateNoticia(noticia);
        }

        [HttpDelete]
        public async Task<bool> DeleteNoticia(int id)
        {
            return await noticias.DeleteNoticia(id);
        }
    }
}