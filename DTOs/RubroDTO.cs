using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.DTO;

public class RubroDTO{
    public RubroDTO(){

    }

    public RubroDTO(Int32 idrubro, string rubro){
        this.idrubro = idrubro;
        this.rubro = rubro;
    }

    public Int32 idrubro { get; set;}

    public string rubro { get; set;}


    public override string ToString() {
        return $"id: {idrubro}, rubro: {rubro}";
    }
}