using CsPharma_V4.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsPharma_V4.Areas.Identity.Data;

public class LoginContexto : IdentityDbContext<User>
{
    public LoginContexto(DbContextOptions<LoginContexto> options)
        : base(options)
    {
    }

    public virtual DbSet <User> UserSet { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.ApplyConfiguration(new UserEntityConfiguration());

        //Esquema donde se creará todo
        builder.HasDefaultSchema("dlk_torrecontrol");

        //Asignamos nombres personalizados de las tablas
        builder.Entity<User>().ToTable("Dlk_cat_acc_empleados");
        builder.Entity<IdentityRole>().ToTable("Dlk_cat_acc_roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("Dlk_cat_acc_empleados_roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("Dlk_cat_acc_claim_roles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("Dlk_cat_acc_claim_empleados");
        builder.Entity<IdentityUserLogin<string>>().ToTable("Dlk_cat_acc_login_empleados");
        builder.Entity<IdentityUserToken<string>>().ToTable("Dlk_cat_acc_token_empleados");
    }
}

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //Campos adicionales de nombre Y apellido
        builder.Property(usuario => usuario.NombreUsuario).HasMaxLength(255);
        builder.Property(usuario => usuario.ApellidosUsuario).HasMaxLength(255);
    }
}

