using Microsoft.EntityFrameworkCore;
using SipinnaBackend2.Models;

public class RubrosDAO{
    
    private readonly Conexiones _context;
    public RubrosDAO(Conexiones context){
        _context = context;
    }

    

}