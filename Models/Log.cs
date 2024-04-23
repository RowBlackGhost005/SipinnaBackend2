using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Log{
    
    public Log(){

    }

    public Log(Int32 idlog,string usuario, string operacion, string fecha, string status){
        this.idlog = idlog;
        this.usuario = usuario;
        this.operacion = operacion;
        this.fecha = fecha;
        this.status = status;
    }

    [Key]
    public Int32 idlog { get; set;}

    [Column(TypeName= "varchar(255)" )]
    public String usuario {get; set;}

    [Column(TypeName = "varchar(255)")]
    public String operacion{get; set;}

    [Column(TypeName = "varchar(255)")]
    public String fecha{get; set;}

    [Column(TypeName = "varchar(255)")]
    public String status{get; set;}

    public override string ToString() {
       return $"Id: {idlog}, usuario: {usuario},operacion: {operacion},fecha: {fecha},status: {status}";
    }
    
}