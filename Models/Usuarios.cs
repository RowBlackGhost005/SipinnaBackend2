using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Usuarios{
    public Usuarios(){

    }

    public Usuarios(Int32 idusuarios, string nombre, string apellidoPaterno,string apellidoMaterno,Int32 tipoUsuario, string email){
        this.idusuarios = idusuarios;
        this.nombre = nombre;
        this.apellidoPaterno = apellidoPaterno;
        this.apellidoMaterno = apellidoMaterno;
        this.tipoUsuario = tipoUsuario;
        this.email = email;
    }

    [Key]
    public Int32 idusuarios { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string nombre { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string apellidoPaterno { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string apellidoMaterno { get; set;}

    [Column(TypeName = "INT")]
    public Int32 tipoUsuario { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string email { get; set;}

    public override string ToString() {
        return $"id: {idusuarios}, nombre: {nombre},apellidoPaterno: {apellidoPaterno}, apellidoMaterno: {apellidoMaterno}, tipoUsuario: {tipoUsuario}, email: {email}";
    }

}