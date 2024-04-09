using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Indicador{
    
    public Indicador(){

    }

    public Indicador(Int32 idindicador, string nombre, string metadato, Dominio dominio){
        this.idindicador = idindicador;
        this.nombre = nombre;
        this.metadato = metadato;
        this.dominioNav = dominio;
    }

    [Key]
    public Int32 idindicador { get; set;}

    [Column(TypeName= "varchar(255)" )]
    public String nombre {get; set;}

    [Column(TypeName = "varchar(255)")]
    public String metadato{get; set;}

    public Int32 dominio{get; set;}
    [ForeignKey(nameof(dominio))]
    public virtual Dominio dominioNav{get; set;}

    public override string ToString() {
       return $"Id: {idindicador}, nombre: {nombre},metadato: {metadato},dominio: {dominioNav}";
    }
}


