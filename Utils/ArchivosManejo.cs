using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;
using System.IO;

namespace SipinnaBackend2.Utils;

public class ArchivosManejo : ControllerBase{

    public ArchivosManejo(){

    }

    public async Task<string> guardarArchivo(IFormFile xls,string carpeta){
        var carpetaLocal = carpeta; 
        var ruta = Path.Combine(carpetaLocal, "");

        if (!Directory.Exists(ruta)){
            Directory.CreateDirectory(ruta);
        }

        var nombreArchivo = $"{Guid.NewGuid()}_{xls.FileName}";
        var rutaCompleta = Path.Combine(ruta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create)){
            await xls.CopyToAsync(stream);
        }

        return rutaCompleta;
    }

    public async Task<string> eliminarArchivo(string nombreArchivo){

        try{
           System.IO.File.Delete(nombreArchivo);
        }catch (Exception ex){
           throw; 
        }

        return "archivo eliminado con exito";
    }

    public async Task<IActionResult> obtenerArchivo(string ruta){
   
        if (System.IO.File.Exists(ruta)){
            var fileBytes = System.IO.File.ReadAllBytes(ruta);
            return File(fileBytes, "application/vnd.ms-excel", "archivo.xls");
        }else{
           throw new FileNotFoundException("No se encontro el archivo", ruta);
        }
    }
}
