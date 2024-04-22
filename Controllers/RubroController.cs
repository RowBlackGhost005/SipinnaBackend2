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
    public class RubroController : ControllerBase
    {
        private readonly RubrosDAO rubrodao;

        public RubroController(RubrosDAO rubrodao)
        {
            this.rubrodao = rubrodao;
        }

        
        // GET: api/Rubro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rubro>>> GetrubroTbl()
        {
            try{
              return Ok(await rubrodao.getRubro());
            }catch(Exception e){
              return BadRequest(e.Message);
            }

        }

        // GET: api/Rubro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rubro>> GetRubro(int id)
        {
            try{
              var rubro = await rubrodao.getRubroId(id);
              return Ok(rubro);
            }catch(Exception e){
              return BadRequest(e.Message);
            }
        }
        
        [HttpGet("rubroindicador/{id}")]
        public async Task<ActionResult<IEnumerable<Rubro>>> GetRubroIndicador(int id)
        {
            try{            
               return Ok(await rubrodao.getRubroIndicadorId(id));
            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        
        // PUT: api/Rubro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Rubro>> PutRubro(int id,[FromForm] string rubro, [FromForm] IFormFile? datos)
        {
            if (!rubrodao.RubroExists(id))
            {
                return BadRequest("No se ha encontrado el rubro");
            }

            Rubro rubroObj = await rubrodao.getRubroId(id);

            var ruta = "";

            if (datos != null){
                ArchivosManejo archivosM = new ArchivosManejo(); 
                archivosM.eliminarArchivo(rubroObj.datos);
                ruta = await archivosM.guardarArchivo(datos,"Datos");
                rubroObj.datos = ruta;
            }

            rubroObj.rubro = rubro;
            
            try{
              var rubroActualizado = await rubrodao.updateRubro(rubroObj);

              return Ok(rubroActualizado);
            }catch(Exception e){
              return BadRequest(e.Message);
            }


        }

        
        // POST: api/Rubro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rubro>> PostRubro([FromForm] string rubro, [FromForm] IFormFile datos)
        {
            if(datos == null){
                return BadRequest("No se envio ningun archivo");
            }

            ArchivosManejo archivosM = new ArchivosManejo();
            var ruta = await archivosM.guardarArchivo(datos,"Datos");            

            Rubro rubroObj = new Rubro(0,rubro,ruta);

            try{
               var rubroCreado = await rubrodao.createRubro(rubroObj);

               return Ok(rubroCreado);
            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("rubroindicador")]
        public async Task<ActionResult<Rubro>> PostRubroIndicador([FromForm] string rubro, [FromForm] IFormFile datos,[FromForm] int idindicador)
        {
            if(datos == null){
                return BadRequest("No se envio ningun archivo");
            }

            ArchivosManejo archivosM = new ArchivosManejo();
            var ruta = await archivosM.guardarArchivo(datos,"Datos");            

            Rubro rubroObj = new Rubro(0,rubro,ruta);

            try{
              var rubroCreado = await rubrodao.createRubroIndicador(rubroObj,idindicador);
              return Ok(rubroCreado);                

            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        

        // DELETE: api/Rubro/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRubro(int id)
        {
            if(!rubrodao.RubroExists(id)){
                return BadRequest("No se encontro el rubro indicado");
            }

            Rubro rubro = await rubrodao.getRubroId(id);

            ArchivosManejo archivosM = new ArchivosManejo();
            archivosM.eliminarArchivo(rubro.datos);

            try{
              var rubroEstado = await rubrodao.deleteRubro(rubro);
              return Ok(rubroEstado);
            }catch(Exception e){
              return BadRequest(e.Message);
            }
            

            
        }

        [HttpGet]
        [Route("descargas")]
        public async Task<IActionResult> downloadDatos(string ruta){
            ArchivosManejo archivosM = new ArchivosManejo();
            return await archivosM.obtenerArchivo(ruta);
        }        
      
    }
}
