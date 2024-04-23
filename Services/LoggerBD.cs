using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

namespace SipinnaBackend2.Services;
public class LoggerBD{

    private readonly Conexiones _context;
    public LoggerBD(Conexiones context){
        _context = context;
    }
        
    public async Task<string> crearLog(string usuario,string operacion, string estado){
        try{
            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            Log log = new Log(0,usuario,operacion,fecha,estado);

            _context.logTbl.Add(log);
            await _context.SaveChangesAsync();

            return "Log creado exitosamente";
            
        }catch(Exception e){
            return "Error al crear log";
        }

    }
}