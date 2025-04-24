using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Models;

namespace Todo.Infrastructure.Mappings
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
            builder.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();
            builder.Property(e => e.Salt).HasColumnName("salt").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(e => e.LastLoginAt).HasColumnName("last_login_at");

            builder.HasMany(e => e.Todos)
                .WithOne(e => e.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}
