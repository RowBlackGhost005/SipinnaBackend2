using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

namespace SipinnaBackend2.DTO;

public class IndicadorDTO{
    
    public IndicadorDTO(){

    }

    public IndicadorDTO(Int32 idindicador, string nombre, Dominio dominio){
        this.idindicador = idindicador;
        this.nombre = nombre;
        this.dominioNav = dominio;
    }


    public Int32 idindicador { get; set;}
    public String nombre {get; set;}
    public Dominio dominioNav{get; set;}
    public int dominio{get; set;}

    public override string ToString() {
       return $"Id: {idindicador}, nombre: {nombre},dominio: {dominioNav}";
    }
}