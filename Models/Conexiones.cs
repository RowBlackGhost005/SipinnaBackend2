//contexto de base de datos

using Microsoft.EntityFrameworkCore;

namespace SipinnaBackend2.Models;

public class Conexiones : DbContext{
    public Conexiones(DbContextOptions<Conexiones> options) :base (options){

    }

    public DbSet<Dominio> dominioTbl {get; set;} = null!;
    public DbSet<Enlaces> enlacesTbl {get;set;} = null!;

    public DbSet<Indicador> indicadorTbl {get;set;} = null!;

    public DbSet<Noticias> noticiasTbl {get;set;} = null!;

    public DbSet<Rubro> rubroTbl {get;set;} = null!;

    public DbSet<RubrosIndicador> rubrosIndicadorTbl {get;set;} = null!;

    public DbSet<Usuarios> usuariosTbl {get;set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //configurar tablas
        modelBuilder.Entity<Dominio>().ToTable("dominio");
        modelBuilder.Entity<Enlaces>().ToTable("enlaces");
        modelBuilder.Entity<Indicador>().ToTable("indicador");
        modelBuilder.Entity<Noticias>().ToTable("noticias");
        modelBuilder.Entity<Rubro>().ToTable("rubro");
        modelBuilder.Entity<RubrosIndicador>().ToTable("rubros-indicador");
        modelBuilder.Entity<Usuarios>().ToTable("usuarios");

  
        
    }


}