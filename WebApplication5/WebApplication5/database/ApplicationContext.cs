using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication5.model;


namespace WebApplication5.database
{
    

    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Users { get; set; } = null!;
        public DbSet<Karta> Kards { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                    new Person { Id = 1, Name = "Tom", Age = 37 },
                    new Person { Id = 2, Name = "Bob", Age = 41 },
                    new Person { Id = 3, Name = "Sam", Age = 24 }
            );
            modelBuilder.Entity<Karta>().HasData(
                    new Karta { Id = 1, NomerKarty = "1111", Skidka = 37, IdPerson= 1 },
                    new Karta { Id = 2, NomerKarty = "1112", Skidka = 37, IdPerson = 2 },
                    new Karta { Id = 3, NomerKarty = "1113", Skidka = 37, IdPerson = 3 },
                    new Karta { Id = 4, NomerKarty = "1114", Skidka = 37, IdPerson = 4 }
            );
        }
    }
}
