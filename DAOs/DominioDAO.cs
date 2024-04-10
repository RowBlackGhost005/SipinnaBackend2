using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

public class DominioDAO{
    
    private readonly Conexiones _context;
    public DominioDAO(Conexiones context){
        _context = context;
    }

    public async Task<IEnumerable<Dominio>> getDominio(){
        try{
            return await _context.dominioTbl.ToListAsync();
        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }
    }

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

    //pendiente
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

    public async Task<Dominio> createDominio(Dominio dominio){

        try{
            _context.dominioTbl.Add(dominio);
            await _context.SaveChangesAsync();

            return dominio;

        }catch(Exception ex){
            throw new InvalidOperationException(ex.Message);
        }

    }

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

    private bool DominioExists(int id){
        return _context.dominioTbl.Any(e => e.iddominio == id);
    }
}