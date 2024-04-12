using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SipinnaBackend2.Models;

public class NoticiasDAO
{
    private readonly Conexiones _context;

    /// <summary>
    /// Crea un objeto de acceso a datos de la entidad Noticias
    /// </summary>
    /// <param name="context">Conexión de base de datos</param>
    public NoticiasDAO(Conexiones context)
    {
        _context = context;
    }


    /// <summary>
    /// Devuelve todas las noticias almacenadas en forma de Lista.
    /// </summary>
    /// <returns>Task con la lista de Noticias</returns>
    /// <exception cref="InvalidOperationException">Excepción si la operación falla</exception>
    public async Task<IEnumerable<Noticias>> GetNoticias()
    {
        try{
            return await _context.noticiasTbl.ToListAsync();
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Devuelve la noticia cuyo id sea igual al dado como parámetro.
    /// </summary>
    /// <param name="id">Noticia a buscar</param>
    /// <returns>Información de la noticia relacionada al ID dado si existe.</returns>
    /// <exception cref="InvalidOperationException">Excepción si no se encuentra la noticia o sucede un error en la búsqueda.</exception>
    public async Task<Noticias> GetNoticiasId(Int32 id)
    {
        try{
            var noticia = await _context.noticiasTbl.FindAsync(id);

            if (noticia != null){
                return noticia;
            }else{
                throw new Exception("No se encontró la noticia con ese id");
            }
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Almacena la noticia dada como parámetro y devuelve el mismo objeto con un ID asignado.
    /// </summary>
    /// <param name="noticia">Noticia a almacenar</param>
    /// <returns>Noticia con el ID asignado</returns>
    /// <exception cref="InvalidOperationException">Excepción si ocurre un error al realizar la consulta</exception>
    public async Task<Noticias> CreateNoticias(Noticias noticia)
    {
        try{
            _context.noticiasTbl.Add(noticia);
            await _context.SaveChangesAsync();

            return noticia;
            
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza la noticia con la información que está dentro de la noticia dada como parámetro siempre y cuando
    /// exista una noticia en la base de datos con el mismo ID.
    /// </summary>
    /// <param name="noticiaUpdate">Datos de la noticia a actualizar</param>
    /// <returns>La noticia actualizada si se actualizó.</returns>
    /// <exception cref="DbUpdateException">Excepción si no se pudo realizar la actualización.</exception>
    public async Task<Noticias> UpdateNoticia(Noticias noticiaUpdate)
    {
        if (await _context.noticiasTbl.FindAsync(noticiaUpdate.idnoticias) is Noticias noticiaDB)
        {
            _context.Entry(noticiaDB).CurrentValues.SetValues(noticiaUpdate);

            await _context.SaveChangesAsync();

            return noticiaDB;
        }else{
            throw new DbUpdateException("No se encontró una noticia con el ID proporcionado");
        }

        /*
        var noticia = _context.noticiasTbl.First(noticiadb => noticiadb.idnoticias == noticiaUpdate.idnoticias);

        if (noticia != null)
        {
            noticia.titulo = noticiaUpdate.titulo;
            noticia.imagen = noticiaUpdate.imagen;
            noticia.enlace = noticiaUpdate.enlace;

            await _context.SaveChanges();

            return noticia;
        }else{
            throw new DbUpdateException("No existe una noticia con el ID dado");
        }
        */
    }

    /// <summary>
    /// Elimina la noticia almacenada cuyo ID coinicda con el ID dado como parámetro.
    /// </summary>
    /// <param name="id">ID de la noticia a eliminar.</param>
    /// <returns>True en caso de eliminarse correctamente.</returns>
    /// <exception cref="Exception">Excepción si existe algún error al eliminar la noticia.</exception>
    public async Task<bool> DeleteNoticia(int id)
    {
        try{
            Noticias noticia = _context.noticiasTbl.Where(b => b.idnoticias == id).First();
            _context.noticiasTbl.Remove(noticia);
            await _context.SaveChangesAsync();

            return true;
        }catch(Exception ex){
            throw new Exception("No fue posible eliminar la noticia con el ID proporcionado" , ex);
        }

    }

    /// <summary>
    /// Consulta si existe una noticia con el ID dado como parámetro.
    /// </summary>
    /// <param name="id">Noticia a buscar.</param>
    /// <returns>True si la noticia existe, False en caso contrairo.</returns>
    private bool NoticiaExists(int id)
    {
        return _context.noticiasTbl.Any(x => x.idnoticias == id);
        
    }

}