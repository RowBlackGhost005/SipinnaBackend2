using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;
using SipinnaBackend2.DTO;

public class RubrosDAO{
    
    private readonly Conexiones _context;
    public RubrosDAO(Conexiones context){
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los rubros almacenados en la base de datos
    /// </summary>
    /// <returns>Rubros en formato de lista</returns>
    /// <exception cref="Exception">Excepcion si ocurre algun error durante la operacion</exception>
    public async Task<IEnumerable<RubroDTO>> getRubro(){
        try{
                var resultadoConsulta = from rubros in _context.rubroTbl
                select new RubroDTO
                {
                    idrubro = rubros.idrubro,
                    rubro = rubros.rubro
                };

            return resultadoConsulta;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene un rubro por su id enviado como parametro
    /// </summary>
    /// <param name="id">Id del rubro</param>
    /// <returns>rubro encontrado</returns>
    /// <exception cref="Exception">Excepcion si no se encuentra el rubro</exception>
    public async Task<Rubro> getRubroId(Int32 id){
        try{
            var rubro = await _context.rubroTbl.FindAsync(id);

            if (rubro == null)
            {
                throw new Exception("no se encontro el rubro con ese id");
            }

            return rubro;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene todos los rubros que esten relacionados a un indicador y a un rubro
    /// Mediante el id del indicador especificado
    /// </summary>
    /// <param name="idindicador">id del indicador con el que se relaciona el rubro</param>
    /// <returns>lista de rubros</returns>
    /// <exception cref="Exception">Excepcion si ocurre un error durante la consulta</exception>
    public async Task<IEnumerable<Rubro>> getRubroIndicadorId(Int32 idindicador){

        try{
            var resultadoConsulta = from rubrosindicador in _context.rubrosIndicadorTbl
                join indicador in _context.indicadorTbl on rubrosindicador.indicador equals indicador.idindicador
                join rubro in _context.rubroTbl on rubrosindicador.rubro equals rubro.idrubro
                where idindicador == rubrosindicador.indicador
                select new Rubro
                {
                    idrubro = rubro.idrubro,
                    rubro = rubro.rubro
                };

            return resultadoConsulta;

        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza un rubro con los datos recibidos en el paremtro
    /// </summary>
    /// <param name="rubro">datos a actualizar</param>
    /// <returns>1 si se realizo la operacion con exito</returns>
    /// <exception cref="Exception">Excepcion si ocurre un error durante la operacion</exception>
    public async Task<Rubro> updateRubro(Rubro rubro){
        try{
            if (!RubroExists(rubro.idrubro))
            {
                throw new Exception("no se encontro el rubro con ese id");
            }
            
            _context.Entry(rubro).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return rubro;
        }catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Almacena un nuevo rubro sin referencia de indicador en la base de datos
    /// </summary>
    /// <param name="rubro">rubro a almacenar</param>
    /// <returns>regresa el rubro creado</returns>
    /// <exception cref="Exception">Excepcion si ocurre algun error durante la transaccion</exception>
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

    /// <summary>
    /// Crea un rubro junto con su referencia de indicador en la base de datos
    /// </summary>
    /// <param name="rubro">rubro a almacenar en la base de datos</param>
    /// <param name="idindicador">id del indicador relacionado a este rubro</param>
    /// <returns>el rubro almacenado en la base de datos</returns>
    /// <exception cref="Exception">Excepcion si ocurre algun error durante la operacion</exception>
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

    /// <summary>
    /// Elimina un rubro de la base de datos (eliminacion en cascasda)
    /// </summary>
    /// <param name="rubro">rubro a eliminar en la base de datos</param>
    /// <returns>regresa un booleano con el estado de la operacion</returns>
    /// <exception cref="Exception">Excepcion si ocurre un error durante la operacion</exception>
    public async Task<bool> deleteRubro(Rubro rubro) {

        try{
          if (!RubroExists(rubro.idrubro)){
              return false;
          }

          _context.rubroTbl.Remove(rubro);
          await _context.SaveChangesAsync();

          return true;
        }catch(Exception ex){
          throw new Exception(ex.Message);
        }

    }

    /// <summary>
    /// Verifica si un rubro dado existe por el parametro de su id
    /// </summary>
    /// <param name="id">id del rubro a verificar</param>
    /// <returns>True si existe, False de lo contrario</returns>
    public bool RubroExists(int id){
        return _context.rubroTbl.Any(e => e.idrubro == id);
    } 

}