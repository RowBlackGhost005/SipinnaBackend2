using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.DTO;

public class DominioDTO{
    public DominioDTO(){

    }

    public DominioDTO(string nombre){
        this.nombre = nombre;    
    }

    public string nombre { get; set;}

    public override string ToString() {
        return $"nombre: {nombre}";
    }

}