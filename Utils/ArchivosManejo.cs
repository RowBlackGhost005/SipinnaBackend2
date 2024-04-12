using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

namespace SipinnaBackend2.Utils;

public class ArchivosManejo{

    public ArchivosManejo(){

    }

    public async Task<string> guardarArchivo(IFormFile xls){
        var carpeta = "Metadatos"; 
        var ruta = Path.Combine(carpeta, "");

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
           File.Delete(nombreArchivo);
        }catch (Exception ex){
           throw; 
        }

        return "archivo eliminado con exito";
    }

    public async Task<FileStream> obtenerArchivo(string ruta){

        if (File.Exists(ruta)){
           var stream = new FileStream(ruta, FileMode.Open);
           return stream;
        }else{
           throw new FileNotFoundException("No se encontro el archivo", ruta);
        }
    }
}
