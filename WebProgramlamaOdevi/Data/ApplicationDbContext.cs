using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaOdevi.Models;

namespace WebProgramlamaOdevi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Projedeki property'ler ile db tablolarındaki kolon isimleri aynı olmayabilir. Bu yüzden veritabanındaki karşılıklarını yazmak zorundayız.
            modelBuilder.Entity<Animal>(a =>
            {
                a.ToTable("Animals").HasKey(p => p.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.Age).HasColumnName("Age");
                a.Property(p => p.AnimalTypeId).HasColumnName("AnimalTypeId");
                a.Property(p => p.Description).HasColumnName("Description");
                a.Property(p => p.isAdopted).HasColumnName("IsAdopted");
                a.Property(p => p.isConfirmed).HasColumnName("IsConfirmed");
                a.HasOne(p => p.AnimalType);
            });
            modelBuilder.Entity<AnimalAdopted>(a =>
            {
                a.ToTable("AnimalAdopteds").HasKey(p => p.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.isConfirmed).HasColumnName("IsConfirmed");
                a.Property(p => p.ConfirmedDateTime).HasColumnName("ConfirmedDateTime");
                a.Property(p => p.AnimalId).HasColumnName("AnimalId");
                a.Property(p => p.CreatedDateTime).HasColumnName("CreatedDateTime");
                a.Property(p => p.isConfirmed).HasColumnName("IsConfirmed");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.HasOne(p => p.Animal);
                a.HasOne(p => p.IdentityUser);
            });
            modelBuilder.Entity<AnimalType>(a =>
            {
                a.ToTable("AnimalTypes").HasKey(p => p.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Animals);
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Animal> Animal { get; set; } = default!;
        public DbSet<AnimalType> AnimalType { get; set; } = default!;
        public DbSet<AnimalAdopted> AnimalAdopted { get; set; } = default!;
    }
}