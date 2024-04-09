using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Rubro{
    public Rubro(){

    }

    public Rubro(Int32 idrubro, string rubro,string datos){
        this.idrubro = idrubro;
        this.rubro = rubro;
        this.datos = datos;    
    }

    [Key]
    public Int32 idrubro { get; set;}

    [Column(TypeName = "varchar(45)")]
    public string rubro { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string datos { get; set;}

    public override string ToString() {
        return $"id: {idrubro}, rubro: {rubro},datos: {datos}";
    }
}