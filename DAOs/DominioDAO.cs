using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

public class DominioDAO{
    
    private readonly Conexiones _context;

    /// <summary>
    /// Crea un objeto de acceso a datos de la entidad de Dominio
    /// </summary>
    /// <param name="context">Conexion de base de datos</param>
    public DominioDAO(Conexiones context){
        _context = context;
    }

    /// <summary>
    /// Devuelve todas los dominios almacenados en la base de datos en formato de lista
    /// </summary>
    /// <returns>Task con la lista de dominios</returns>
    /// <exception cref="InvalidOperationException">Excepcion si la operacion falla</exception>
    public async Task<IEnumerable<Dominio>> getDominio(){
        try{
            return await _context.dominioTbl.ToListAsync();
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

   /// <summary>
   /// Devuelve el dominio que coincida con el id enviado como parametro
   /// </summary>
   /// <param name="id">id del dominio a buscar</param>
   /// <returns>Informacion del dominio si existe</returns>
   /// <exception cref="InvalidOperationException">Excepcion si no encuentra el dominio</exception>
    public async Task<Dominio> getDominioId(Int32 id){
        try{
            var dominio = await _context.dominioTbl.FindAsync(id);

            if (!DominioExists(id))
            {
                throw new Exception("no se encontro el dominio con ese id");
            }

            return dominio;
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza el dominio con la informacion enviada 
    /// </summary>
    /// <param name="dominio">Datos del dominio a actualizar</param>
    /// <returns>Dominio actualizado</returns>
    /// <exception cref="InvalidOperationException">Excepcion si no se pudo realizar la operacion</exception>
    public async Task<int> updateDominio(Dominio dominio){
        try{
            if (!DominioExists(dominio.iddominio))
            {
                throw new Exception("no se encontro el dominio con ese id");
            }
            
            _context.Entry(dominio).State = EntityState.Modified;

            var dominioActualizado = await _context.SaveChangesAsync();

            return dominioActualizado;
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Crea un objeto dominio con la informacion recibida
    /// </summary>
    /// <param name="dominio">objeto dominio a crear</param>
    /// <returns>objeto creado en la base de datos</returns>
    /// <exception cref="InvalidOperationException">Excepsion si no se pudo realizar la operacion</exception>
    public async Task<Dominio> createDominio(Dominio dominio){

        try{
            _context.dominioTbl.Add(dominio);
            await _context.SaveChangesAsync();

            return dominio;

        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }

    }

    /// <summary>
    /// Elimina un elemento dominio dentro de la base de datos que coincida con el id
    /// </summary>
    /// <param name="id">id del objeto dominio a eliminar</param>
    /// <returns>string con el resultado de la operacion</returns>
    /// <exception cref="InvalidOperationException">Excepcion si no se pudo realizar la operacion</exception>
    public async Task<string> deleteDominio(int id) {

        try{
          if (!DominioExists(id)){
              return "no se encontro el dominio";
          }

          Dominio dominio = new Dominio(id,"irrelevante");

          _context.dominioTbl.Remove(dominio);
          await _context.SaveChangesAsync();

          return "eliminado con exito";
        }catch(Exception ex){
          throw new InvalidOperationException(ex.Message);
        }

    }

    /// <summary>
    /// Verifica si el dominio del id enviado existe
    /// </summary>
    /// <param name="id">id del dominio a verificar</param>
    /// <returns>true si existe,false en caso contrario</returns>
    private bool DominioExists(int id){
        return _context.dominioTbl.Any(e => e.iddominio == id);
    }
}