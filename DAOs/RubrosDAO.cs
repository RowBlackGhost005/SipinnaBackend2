using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

public class RubrosDAO{
    
    private readonly Conexiones _context;
    public RubrosDAO(Conexiones context){
        _context = context;
    }

    public async Task<IEnumerable<Rubro>> getRubro(){
        try{
            return await _context.rubroTbl.ToListAsync();
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<Rubro> getRubroId(Int32 id){
        try{
            var rubro = await _context.rubroTbl.FindAsync(id);

            if (!RubroExists(id))
            {
                throw new Exception("no se encontro el rubro con ese id");
            }

            return rubro;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Rubro>> getRubroIndicadorId(Int32 idindicador){
        try{
            var resultadoConsulta = from rubrosindicador in _context.rubrosIndicadorTbl
                join indicador in _context.indicadorTbl on rubrosindicador.indicador equals indicador.idindicador
                join rubro in _context.rubroTbl on rubrosindicador.rubro equals rubro.idrubro
                where idindicador == rubrosindicador.indicador
                select new Rubro
                {
                    idrubro = rubro.idrubro,
                    rubro = rubro.rubro,
                    datos = rubro.datos
                };

            return resultadoConsulta;

        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    //pendiente
    public async Task<int> updateRubro(Rubro rubro){
        try{
            if (!RubroExists(rubro.idrubro))
            {
                throw new Exception("no se encontro el rubro con ese id");
            }
            
            _context.Entry(rubro).State = EntityState.Modified;

            var rubroActualizado = await _context.SaveChangesAsync();

            return rubroActualizado;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    
    public async Task<Rubro> createRubro(Rubro rubro){
        //TODO: create rubroindicador
        try{

            _context.rubroTbl.Add(rubro);
            await _context.SaveChangesAsync();

            return rubro;

        }catch(Exception ex){
            throw new Exception(ex.Message);
        }

    }

    public async Task<Rubro> createRubroIndicador(Rubro rubro,int idindicador){
        //TODO: create rubroindicador
        try{

            //crea automaticamente un rubro y un rubro indicador
            Indicador indicador = await _context.indicadorTbl.FindAsync(idindicador);

            if(indicador == null){
                throw new Exception("No se encontro el indicador");
            }

            RubrosIndicador rubroindicador = new RubrosIndicador(0,indicador,rubro);

            _context.rubrosIndicadorTbl.Add(rubroindicador);
            await _context.SaveChangesAsync();

            return rubro;

        }catch(Exception ex){
            throw new Exception(ex.Message);
        }

    }    

    public async Task<string> deleteRubro(Rubro rubro) {

        try{
          if (!RubroExists(rubro.idrubro)){
              return "no se encontro el rubro";
          }

          _context.rubroTbl.Remove(rubro);
          await _context.SaveChangesAsync();

          return "eliminado con exito";
        }catch(Exception ex){
          throw new Exception(ex.Message);
        }

    }

    public bool RubroExists(int id){
        return _context.rubroTbl.Any(e => e.idrubro == id);
    } 

}