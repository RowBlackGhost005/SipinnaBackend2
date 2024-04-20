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
        public async Task<ActionResult<IEnumerable<Indicador>>> GetindicadorTbl()
        {
            try{
               return Ok(await indicadordao.getIndicador());
            }catch(Exception e){
               return BadRequest(e.Message);
            }
            
        }

        // GET: api/Indicador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Indicador>> GetIndicador(int id)
        {
           try{
                var indicador = await indicadordao.getIndicadorId(id);
                return Ok(indicador);
           }catch(Exception e){
                return BadRequest(e.Message);
           }

        }

        // PUT: api/Indicador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Indicador>> PutIndicador(int id,[FromForm] string nombre, [FromForm] IFormFile metadato, [FromForm] string dominioNavId)
        {
            if(!indicadordao.IndicadorExists(id)){
                return BadRequest("No se encontro el indicador");
            }

            if (metadato == null){
                return BadRequest("No se envio el archivo de metadatos");
            }

            Indicador indicador = await indicadordao.getIndicadorId(id);

            ArchivosManejo archivosM = new ArchivosManejo(); 
            archivosM.eliminarArchivo(indicador.metadato);
            var ruta = await archivosM.guardarArchivo(metadato,"Metadatos");

            Dominio dominio = new Dominio(int.Parse(dominioNavId),""); 
            indicador.nombre = nombre;
            indicador.metadato = ruta;
            indicador.dominioNav = dominio;

            try{
               var indicadorActualizado = await indicadordao.updateIndicador(indicador);

               return Ok(indicadorActualizado);                

            }catch(Exception e){
               return BadRequest(e.Message);
            }

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
            var ruta = await archivosM.guardarArchivo(metadato,"Metadatos");
            
            Dominio dominio = new Dominio(int.Parse(dominioNavId),""); 
            Indicador indicador = new Indicador(0,nombre,ruta,dominio);
           
            try{
               var indicadorCreado = await indicadordao.createIndicador(indicador);

               return Ok(indicadorCreado);
            }catch(Exception e){
               return BadRequest(e.Message);
            }

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
            
            try{
               await indicadordao.deleteIndicador(indicador);
               return Ok(true);                

            }catch(Exception e){
               return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("descargas")]
        public async Task<IActionResult> downloadMetadato(string ruta){
            ArchivosManejo archivosM = new ArchivosManejo();
            return await archivosM.obtenerArchivo(ruta);
        }

        

    }
}
