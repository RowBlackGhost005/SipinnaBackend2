using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Dominio{
    public Dominio(){

    }

    public Dominio(Int32 iddominio, string nombre){
        this.iddominio = iddominio;
        this.nombre = nombre;    
    }

    [Key]
    public Int32 iddominio { get; set;}

    [Column(TypeName = "varchar(120)")]
    public string nombre { get; set;}

    public override string ToString() {
        return $"id: {iddominio}, nombre: {nombre}";
    }

}