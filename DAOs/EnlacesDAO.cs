using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SipinnaBackend2.Models;


public class EnlacesDAO
{
    private readonly Conexiones _context;

    /// <summary>
    /// Crea un objeto de acceso a datos de la entidad Enlaces
    /// </summary>
    /// <param name="context">Conexion a base de datos</param>
    public EnlacesDAO(Conexiones context)
    {
        _context = context;
    }

    /// <summary>
    /// Consulta todas los enalces almacenados y los ordena en una lista.
    /// </summary>
    /// <returns>Task con la lista de Enlaces</returns>
    /// <exception cref="InvalidOperationException">Excepción si la operación falla</exception>
    public async Task<IEnumerable<Enlaces>> GetEnlaces()
    {
        try{
            return await _context.enlacesTbl.ToListAsync();
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Búsca el enlace cuyo ID sea igual al ID dado como parámetro.
    /// </summary>
    /// <param name="id">ID del enlace a buscar.</param>
    /// <returns>Task conteniendo el Enlace consultado si se encontró.</returns>
    /// <exception cref="InvalidOperationException">Excepción si ocurre un error al consultar el enlace.</exception>
    public async Task<Enlaces> GetEnlacesId(Int32 id)
    {
        try{
            var enlace = await _context.enlacesTbl.FindAsync(id);

            if(enlace == null){
                throw new Exception("No se encontró el enlace con ese ID");
            }

            return enlace;

        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Almacena el enlace dado como parámetro y actualiza el objeto agregando el ID asignado.
    /// </summary>
    /// <param name="enlace">Enlace a almacenar.</param>
    /// <returns>Objeto Enlace con su ID asignado.</returns>
    /// <exception cref="InvalidOperationException">Excepción si ocurre un error al realizar la consulta.</exception>
    public async Task<Enlaces> CreateEnlace(Enlaces enlace)
    {
        try{
            _context.enlacesTbl.Add(enlace);
            await _context.SaveChangesAsync();

            return enlace;
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza un enlace con la información del enlace dado como parámetro si existe almacenado
    /// un enlace con el mismo ID.
    /// </summary>
    /// <param name="enlaceUpdate">Enlace con la información a actualizar.</param>
    /// <returns>Objeto Enlace en el estado en el que se guardó</returns>
    /// <exception cref="DbUpdateException">Excepción si no se encontró el enlace en la base de datos</exception>
    public async Task<Enlaces> UpdateEnlace(Enlaces enlaceUpdate)
    {
        if(await _context.enlacesTbl.FindAsync(enlaceUpdate.idenlaces) is Enlaces enlaceDB)
        {
            _context.Entry(enlaceDB).CurrentValues.SetValues(enlaceUpdate);

            await _context.SaveChangesAsync();

            return enlaceDB;
        }else{
            throw new DbUpdateException("No se encontró un enlace con el ID proporcionado");
        }
    }

    /// <summary>
    /// Elimina el enlace almaceado cuyo ID coincida con el ID dado como parámetro.
    /// </summary>
    /// <param name="id">ID del enlace a eliminar.</param>
    /// <returns>True en caso de eliminarse correctamente.</returns>
    /// <exception cref="Exception">Excepción si existe algún error al eliminar el enlace.</exception>
    public async Task<bool> DeleteEnlace(int id)
    {
        try{
            Enlaces enlace = _context.enlacesTbl.Where(b => b.idenlaces == id).First();
            _context.enlacesTbl.Remove(enlace);

            await _context.SaveChangesAsync();
            return true;
        }catch(Exception ex){
            throw new Exception("No fue posible eliminar el enlace con el ID proporcionado", ex);
        }
    }
}
