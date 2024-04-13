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
        public async Task<IEnumerable<Rubro>> GetrubroTbl()
        {
            return await rubrodao.getRubro();
        }

        // GET: api/Rubro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rubro>> GetRubro(int id)
        {
            var rubro = await rubrodao.getRubroId(id);

            return rubro;
        }
        

        [HttpGet("rubroindicador/{id}")]
        public async Task<IEnumerable<Rubro>> GetRubroIndicador(int id)
        {
            return await rubrodao.getRubroIndicadorId(id);
        }

        
        // PUT: api/Rubro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<int> PutRubro(int id,[FromForm] string rubro, [FromForm] IFormFile datos)
        {
            if (!rubrodao.RubroExists(id))
            {
                return 0;
            }

            if (datos == null){
                return 0;
            }            

            Rubro rubroObj = await rubrodao.getRubroId(id);

            ArchivosManejo archivosM = new ArchivosManejo(); 
            archivosM.eliminarArchivo(rubroObj.datos);

            var ruta = await archivosM.guardarArchivo(datos,"Datos");

            rubroObj.rubro = rubro;
            rubroObj.datos = ruta;

            var rubroActualizado = await rubrodao.updateRubro(rubroObj);

            return rubroActualizado;
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
            var rubroCreado = await rubrodao.createRubro(rubroObj);

            return rubroCreado;
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
            var rubroCreado = await rubrodao.createRubroIndicador(rubroObj,idindicador);

            return rubroCreado;
        }

        

        // DELETE: api/Rubro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRubro(int id)
        {
            if(!rubrodao.RubroExists(id)){
                return BadRequest();
            }

            Rubro rubro = await rubrodao.getRubroId(id);

            ArchivosManejo archivosM = new ArchivosManejo();
            archivosM.eliminarArchivo(rubro.datos);

            await rubrodao.deleteRubro(rubro);

            return NoContent();
        }

        [HttpGet]
        [Route("descargas")]
        public async Task<IActionResult> downloadDatos(string ruta){
            ArchivosManejo archivosM = new ArchivosManejo();
            return await archivosM.obtenerArchivo(ruta);
        }        
      
    }
}
