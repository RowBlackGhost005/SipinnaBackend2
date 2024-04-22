using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

public class IndicadorDAO{
    
    private readonly Conexiones _context;
    public IndicadorDAO(Conexiones context){
        _context = context;
    }

    /// <summary>
    /// Devuelve una lista de todos los objetos indicador en la base de datos
    /// </summary>
    /// <returns>Lista de objetos indicador</returns>
    /// <exception cref="Exception">Excepcion en caso de operacion fallida</exception>
    public async Task<IEnumerable<Indicador>> getIndicador(){
        try{
            return await _context.indicadorTbl.ToListAsync();
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Devuelve un indicador que coincida con el parametro id recibido
    /// </summary>
    /// <param name="id">id del indicador a buscar</param>
    /// <returns>Indicador si existe</returns>
    /// <exception cref="Exception">Excepcion si no se encuentra el indicador o la operacion falla</exception>
    public async Task<Indicador> getIndicadorId(Int32 id){
        try{
            var indicador = await _context.indicadorTbl.FindAsync(id);

            if(indicador == null){
                throw new Exception("No existe indicador con id especificado");
            }

            return indicador;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza un indicador con los datos recibidos
    /// </summary>
    /// <param name="indicador">Datos recibidos a actualizar</param>
    /// <returns>1 si se realizo la actualizacion con exito</returns>
    /// <exception cref="Exception">Excepcion si la operacion falla o no se encontro el indicador</exception>
    public async Task<Indicador> updateIndicador(Indicador indicador){
        try{            
            indicador.dominioNav = await _context.dominioTbl.FindAsync(indicador.dominioNav.iddominio);
            
            _context.Entry(indicador).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return indicador;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Almacena un nuevo registro de un indicador en la base de datos
    /// </summary>
    /// <param name="indicador">datos a almacenar en la base de datos</param>
    /// <returns>indicador almacenado</returns>
    /// <exception cref="Exception">Excepcion si ocurre algun error durante el proceso</exception>
    public async Task<Indicador> createIndicador(Indicador indicador){

        try{
            indicador.dominioNav = await _context.dominioTbl.FindAsync(indicador.dominioNav.iddominio);
            _context.indicadorTbl.Add(indicador);
            await _context.SaveChangesAsync();

            return indicador;

        }catch(Exception ex){
            throw new Exception(ex.Message);
        }

    }

    /*
    /// <summary>
    /// Elimina un indicador de la base de datos
    /// </summary>
    /// <param name="indicador">indicador a eliminar</param>
    /// <returns>regresa string con estado de la operacion</returns>
    /// <exception cref="Exception">Excepcion si ocurre algun error durante la operacion</exception>
    public async Task<bool> deleteIndicador(Indicador indicador) {

        try{
          if (!IndicadorExists(indicador.idindicador)){
              return false;
          }

          _context.indicadorTbl.Remove(indicador);
          await _context.SaveChangesAsync();

          return true;
        }catch(Exception ex){
          throw new Exception(ex.Message);
        }

    }
    */

    /// <summary>
    /// Verifica si el indicador con el id enviado existe
    /// </summary>
    /// <param name="id">Id del indicador a verificar</param>
    /// <returns>true si existe, false al contrario</returns>
    public bool IndicadorExists(int id){
        return _context.indicadorTbl.Any(e => e.idindicador == id);
    }
}