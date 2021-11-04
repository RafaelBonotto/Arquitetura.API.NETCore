using Arquitetura.API.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arquitetura.API.Infraestruture.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("TB_CURSO");
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Codigo).ValueGeneratedOnAdd();// GERAR VALORES PARA ESSA PROPRIEDADE (AUTOINCREMENT)
            builder.Property(X => X.Nome);
            builder.Property(X => X.Descricao);
            builder.HasOne(x => x.Usuario)
                .WithMany().HasForeignKey(fk => fk.CodigoUsuario);
        }
    }
}
