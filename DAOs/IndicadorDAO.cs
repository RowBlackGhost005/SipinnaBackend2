using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

public class IndicadorDAO{
    
    private readonly Conexiones _context;
    public IndicadorDAO(Conexiones context){
        _context = context;
    }

    public async Task<IEnumerable<Indicador>> getIndicador(){
        try{
            return await _context.indicadorTbl.ToListAsync();
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<Indicador> getIndicadorId(Int32 id){
        try{
            var indicador = await _context.indicadorTbl.FindAsync(id);

            if (!IndicadorExists(id))
            {
                throw new Exception("no se encontro el indicador con ese id");
            }

            return indicador;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    //pendiente
    public async Task<int> updateIndicador(Indicador indicador){
        try{
            if (!IndicadorExists(indicador.idindicador))
            {
                throw new Exception("no se encontro el indicador con ese id");
            }
            
            indicador.dominioNav = await _context.dominioTbl.FindAsync(indicador.dominioNav.iddominio);
            
            _context.Entry(indicador).State = EntityState.Modified;

            var indicadorActualizado = await _context.SaveChangesAsync();

            return indicadorActualizado;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

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

    public async Task<string> deleteIndicador(Indicador indicador) {

        try{
          if (!IndicadorExists(indicador.idindicador)){
              return "no se encontro el indicador";
          }

          _context.indicadorTbl.Remove(indicador);
          await _context.SaveChangesAsync();

          return "eliminado con exito";
        }catch(Exception ex){
          throw new Exception(ex.Message);
        }

    }

    public bool IndicadorExists(int id){
        return _context.indicadorTbl.Any(e => e.idindicador == id);
    }
}