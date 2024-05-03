using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;
using SipinnaBackend2.Utils;
using SipinnaBackend2.Services;
using SipinnaBackend2.DTO;

namespace APISipinnaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadorController : ControllerBase
    {
        public readonly IndicadorDAO indicadordao;

        public readonly LoggerBD logger;

        public IndicadorController(IndicadorDAO indicadordao,LoggerBD logger)
        {
            this.indicadordao = indicadordao;
            this.logger = logger;
        }

        // GET: api/Indicador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndicadorDTO>>> GetindicadorTbl()
        {
            try{
               return Ok(await indicadordao.getIndicador());
            }catch(Exception e){
               return BadRequest(e.Message);
            }
            
        }

        // GET: api/Indicador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IndicadorDTO>> GetIndicador(int id)
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
        public async Task<ActionResult<Indicador>> PutIndicador(int id,[FromForm] string nombre, [FromForm] IFormFile? metadato, [FromForm] string dominioNavId)
        {
            //verifica si indicador existe
            if(!indicadordao.IndicadorExists(id)){
                await this.logger.crearLog("Usuario generico","Actualizacion de indicador","Error: "+"indicador no encontrado");
                return BadRequest("No se encontro el indicador");
            }

            //obtiene el objeto indicador de la bd
            Indicador indicador = await indicadordao.getIndicadorId(id);
       
            //si se envio un archivo entonces crea el nuevo archivo en el servidor
            //y actualiza la ruta en la base de datos
            var ruta = "";

            if (metadato != null){
                ArchivosManejo archivosM = new ArchivosManejo(); 
                archivosM.eliminarArchivo(indicador.metadato);
                ruta = await archivosM.guardarArchivo(metadato,"Metadatos");
                indicador.metadato = ruta;
            }

            Dominio dominio = new Dominio(int.Parse(dominioNavId),"",true); 
            indicador.nombre = nombre;
            indicador.dominioNav = dominio;

            try{
               var indicadorActualizado = await indicadordao.updateIndicador(indicador);

               await this.logger.crearLog("Usuario generico","Actualizacion de indicador","Exito: Se actualizo el sig: "+id);
               return Ok(indicadorActualizado);                

            }catch(Exception e){
               await this.logger.crearLog("Usuario generico","Actualizacion de indicador","Error: "+e.Message);
               return BadRequest(e.Message);
            }

        }

        // POST: api/Indicador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Indicador>> PostIndicador([FromForm] string nombre, [FromForm] IFormFile metadato, [FromForm] string dominioNavId)
        {
            if (metadato == null){
                await this.logger.crearLog("Usuario generico","Creacion de indicador","Error: "+"no se envio un archivo de indicador");
                return BadRequest("No se envio ningun archivo");
            }

            ArchivosManejo archivosM = new ArchivosManejo();
            var ruta = await archivosM.guardarArchivo(metadato,"Metadatos");
            
            Dominio dominio = new Dominio(int.Parse(dominioNavId),"",true); 
            Indicador indicador = new Indicador(0,nombre,ruta,dominio);
           
            try{
               var indicadorCreado = await indicadordao.createIndicador(indicador);

               await this.logger.crearLog("Usuario generico","Creacion de indicador","Exito: Se agrego el sig: "+indicadorCreado.idindicador);
               return Ok(indicadorCreado);
            }catch(Exception e){
                await this.logger.crearLog("Usuario generico","Creacion de indicador","Error: "+e.Message);
               return BadRequest(e.Message);
            }

        }

        /*
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
        */

        [HttpGet]
        [Route("descargas")]
        public async Task<IActionResult> downloadMetadato(int id){

            try{
                Indicador indicador = await indicadordao.getIndicadorId(id);

                ArchivosManejo archivosM = new ArchivosManejo();
                return await archivosM.obtenerArchivo(indicador.metadato);

            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        

    }
}
