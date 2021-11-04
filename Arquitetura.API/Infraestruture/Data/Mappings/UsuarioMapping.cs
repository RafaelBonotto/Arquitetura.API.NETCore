using Arquitetura.API.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arquitetura.API.Infraestruture.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO");
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Codigo).ValueGeneratedOnAdd();// GERAR VALORES PARA ESSA PROPRIEDADE(AUTOINCREMENT)
            builder.Property(X => X.Login);
            builder.Property(X => X.Email);
            builder.Property(X => X.Senha);
        }
    }
}
