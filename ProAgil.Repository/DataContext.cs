using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using  Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Repository.Data
{
    public class DataContext: IdentityDbContext<User,Role,int,IdentityUserClaim<int>,UserRole,IdentityUserLogin<int>,
                                                IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}
        public DbSet<Evento> Eventos{get;set;}
        public DbSet<Palestrante> Palestrantes{get;set;}
        public DbSet<RedeSocial> RedeSociais{get;set;} 
        public DbSet<Lote> Lotes{get;set;}
        public DbSet<Palestrante_evento> Palestrante_eventos{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<UserRole>(userRole=>{
                userRole.HasKey(u => new {u.UserId,u.RoleId});
                userRole.HasOne(u => u.Role).WithMany(r=>r.UserRoles).HasForeignKey(u=>u.RoleId).IsRequired();
                userRole.HasOne(u => u.User).WithMany(r=>r.UserRoles).HasForeignKey(u=>u.UserId).IsRequired();
            });
            
            modelBuilder.Entity<Palestrante_evento>().HasKey(x => new{
                x.PalestranteId,x.EventoId
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}