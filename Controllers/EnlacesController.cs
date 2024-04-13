using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SipinnaBackend2.Models;

namespace APISipinnaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnlacesController : Controller
    {
        public readonly EnlacesDAO enlaces;

        public EnlacesController(EnlacesDAO enlaces)
        {
            this.enlaces = enlaces;
        }

        [HttpGet]
        public async Task<IEnumerable<Enlaces>> GetEnlaces()
        {
            return await enlaces.GetEnlaces();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enlaces>> GetEnlace(int id)
        {
            return await enlaces.GetEnlacesId(id);
        }

        [HttpPost]
        public async Task<ActionResult<Enlaces>> PostEnlace(Enlaces enlace)
        {
            return await enlaces.CreateEnlace(enlace);
        }

        [HttpPut]
        public async Task<ActionResult<Enlaces>> PutEnlace(Enlaces enlace)
        {
            return await enlaces.UpdateEnlace(enlace);
        }

        [HttpDelete]
        public async Task<bool> DeleteNoticia(int id)
        {
            return await enlaces.DeleteEnlace(id);
        }
    }
}