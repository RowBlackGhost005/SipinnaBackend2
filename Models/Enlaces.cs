using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Enlaces{
    public Enlaces(){

    }

    public Enlaces(Int32 idenlaces, string titulo, string enlace){
        this.idenlaces = idenlaces;
        this.titulo = titulo;
        this.enlace = enlace;
    }

    [Key]
    public Int32 idenlaces { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string titulo { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string enlace { get; set;}

    public override string ToString() {
        return $"id: {idenlaces}, titulo: {titulo}, enlace: {enlace}";
    }

}