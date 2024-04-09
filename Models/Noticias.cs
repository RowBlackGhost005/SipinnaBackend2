using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Noticias{
    public Noticias(){

    }

    public Noticias(Int32 idnoticias, string titulo, string imagen,string enlace){
        this.idnoticias = idnoticias;
        this.titulo = titulo;
        this.imagen = imagen;
        this.enlace = enlace;
    }

    [Key]
    public Int32 idnoticias { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string titulo { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string imagen { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string enlace { get; set;}

    public override string ToString() {
        return $"id: {idnoticias}, titulo: {titulo},imagen: {imagen}, enlace: {enlace}";
    }

}