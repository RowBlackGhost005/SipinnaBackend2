using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;
using SipinnaBackend2.DTO;

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
        public async Task<ActionResult<IEnumerable<Dominio>>> GetdominioTbl()
        {  
            return Ok(await dominiodao.getDominio());
        }

        // GET: api/Dominio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dominio>> GetDominio(int id)
        {
            try{
               var dominio = await dominiodao.getDominioId(id);

                if(dominio == null){
                  return NotFound();
                }

               return Ok(dominio);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
            


        }

        // PUT: api/Dominio/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Dominio>> PutDominio(DominioDTO dominio,int id){

            try{
              var dominioActualizado = await dominiodao.updateDominio(dominio,id);

              return Ok(dominioActualizado);
            }catch(Exception e){
                if(!dominiodao.DominioExists(id)){
                    return NotFound();
                }else{
                    return BadRequest(e.Message);
                }
                
            }
            
            
   
        }

        // POST: api/Dominio
        [HttpPost]
        public async Task<ActionResult<Dominio>> PostDominio(DominioDTO dominiodto)
        {

            try{
                Dominio dominio = new Dominio(0,dominiodto.nombre);

                var dominioCreado = await dominiodao.createDominio(dominio);

                return Ok(dominioCreado);    

            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        /*
        // DELETE: api/Dominio/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> deleteDominio(int id)
        {
            try{
                var estado = await dominiodao.deleteDominio(id);

               if(estado == false){
                  return NotFound();
                }
            
                return Ok(true);
                
            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }
        */

    }
}
