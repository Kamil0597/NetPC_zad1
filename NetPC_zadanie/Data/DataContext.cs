using Microsoft.EntityFrameworkCore;
using NetPC_zadanie.Models;

namespace NetPC_zadanie.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        /*
         * Konfiguracja modelu baz danych.
         * Definiuje klucz główny dla encji Contact oraz ustawia automatyczne generowanie wartości Id.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Contact>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
