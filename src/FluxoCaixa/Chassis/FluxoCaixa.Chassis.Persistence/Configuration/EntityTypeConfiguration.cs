using FluxoCaixa.Chassis.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace FluxoCaixa.Chassis.Persistence.Configuration
{
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasValueGenerator(typeof(GuidValueGenerator));
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
        }
    }
}
