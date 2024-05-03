using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.DTO;

public class DominioDTO{
    public DominioDTO(){

    }

    public DominioDTO(string nombre,bool estado){
        this.nombre = nombre;    
        this.estado = estado;
    }

    public string nombre { get; set;}
    public bool estado { get; set;}

    public override string ToString() {
        return $"nombre: {nombre},estado: {estado}";
    }

}