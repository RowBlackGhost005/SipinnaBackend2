using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;
using SipinnaBackend2.Utils;

namespace APISipinnaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadorController : ControllerBase
    {
        public readonly IndicadorDAO indicadordao;

        

        public IndicadorController(IndicadorDAO indicadordao)
        {
            this.indicadordao = indicadordao;
            
        }

        // GET: api/Indicador
        [HttpGet]
        public async Task<IEnumerable<Indicador>> GetindicadorTbl()
        {
            return await indicadordao.getIndicador();
        }

        // GET: api/Indicador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Indicador>> GetIndicador(int id)
        {
           var indicador = await indicadordao.getIndicadorId(id);

           return indicador;
        }

        // PUT: api/Indicador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<int> PutIndicador(int id,[FromForm] string nombre, [FromForm] IFormFile metadato, [FromForm] string dominioNavId)
        {
            if(!indicadordao.IndicadorExists(id)){
                return 0;
            }

            if (metadato == null){
                return 0;
            }

            Indicador indicador = await indicadordao.getIndicadorId(id);

            ArchivosManejo archivosM = new ArchivosManejo(); 
            archivosM.eliminarArchivo(indicador.metadato);
            var ruta = await archivosM.guardarArchivo(metadato);

            Dominio dominio = new Dominio(int.Parse(dominioNavId),""); 
            indicador.nombre = nombre;
            indicador.metadato = ruta;
            indicador.dominioNav = dominio;

            var indicadorActualizado = await indicadordao.updateIndicador(indicador);

            return indicadorActualizado;
        }

        // POST: api/Indicador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Indicador>> PostIndicador([FromForm] string nombre, [FromForm] IFormFile metadato, [FromForm] string dominioNavId)
        {
            if (metadato == null){
                return BadRequest("No se envio ningun archivo");
            }

            ArchivosManejo archivosM = new ArchivosManejo();
            var ruta = await archivosM.guardarArchivo(metadato);
            
           Dominio dominio = new Dominio(int.Parse(dominioNavId),""); 
           Indicador indicador = new Indicador(0,nombre,ruta,dominio);
           var indicadorCreado = await indicadordao.createIndicador(indicador);

           return indicadorCreado;
        }

        // DELETE: api/Indicador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndicador(int id)
        {
            if(!indicadordao.IndicadorExists(id)){
                return BadRequest();
            }

            Indicador indicador = await indicadordao.getIndicadorId(id);

            ArchivosManejo archivosM = new ArchivosManejo();
            archivosM.eliminarArchivo(indicador.metadato);

            await indicadordao.deleteIndicador(indicador);

            return NoContent();
        }

        [HttpGet]
        [Route("descargas")]
        public async Task<FileStream> downloadMetadato(string ruta){
            ArchivosManejo archivosM = new ArchivosManejo();
            return await archivosM.obtenerArchivo(ruta);
        }

        

    }
}
