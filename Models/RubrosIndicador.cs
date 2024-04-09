using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

[Table("rubros-indicador")]
public class RubrosIndicador{
    
    public RubrosIndicador(){

    }

    public RubrosIndicador(Int32 idrubrosIndicador,Indicador indicador, Rubro rubro){
        this.idrubrosIndicador = idrubrosIndicador;
        this.indicadorNav = indicador;
        this.rubroNav = rubro;
    }

    [Key]
    [Column("idrubros-indicador")]
    public Int32 idrubrosIndicador { get; set;}

    public Int32 indicador{get; set;}
    [ForeignKey(nameof(indicador))]
    public virtual Indicador indicadorNav{get; set;}

    public Int32 rubro{get; set;}
    [ForeignKey(nameof(rubro))]
    public virtual Rubro rubroNav{get; set;}

    public override string ToString() {
        return $"Id: {idrubrosIndicador}, indicador: {indicadorNav},rubro: {rubroNav}";
    }
}


