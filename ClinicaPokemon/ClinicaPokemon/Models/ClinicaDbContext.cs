using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClinicaPokemon.Models
{
    public partial class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext()
            : base("name=ClinicaDbContext")
        {
        }

        public virtual DbSet<Animali> Animali { get; set; }
        public virtual DbSet<Armadietti> Armadietti { get; set; }
        public virtual DbSet<Cassetti> Cassetti { get; set; }
        public virtual DbSet<DettagliVendita> DettagliVendita { get; set; }
        public virtual DbSet<DittaFornitrice> DittaFornitrice { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<Ricoveri> Ricoveri { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UsoProdotti> UsoProdotti { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }
        public virtual DbSet<Vendite> Vendite { get; set; }
        public virtual DbSet<Visite> Visite { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animali>()
                .HasMany(e => e.Ricoveri)
                .WithRequired(e => e.Animali)
                .HasForeignKey(e => e.FK_idAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Animali>()
                .HasMany(e => e.Visite)
                .WithRequired(e => e.Animali)
                .HasForeignKey(e => e.FK_idAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Armadietti>()
                .HasMany(e => e.Cassetti)
                .WithRequired(e => e.Armadietti)
                .HasForeignKey(e => e.FK_idArmadietto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Armadietti>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Armadietti)
                .HasForeignKey(e => e.FK_idArmadietto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cassetti>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Cassetti)
                .HasForeignKey(e => e.FK_idCassetto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DittaFornitrice>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.DittaFornitrice)
                .HasForeignKey(e => e.FK_idDittaFornitrice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.DettagliVendita)
                .WithRequired(e => e.Prodotti)
                .HasForeignKey(e => e.FK_idProdotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ricoveri>()
                .Property(e => e.PrezzoRicovero)
                .HasPrecision(19, 4);

            modelBuilder.Entity<UsoProdotti>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.UsoProdotti)
                .HasForeignKey(e => e.FK_idUsoProdotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Animali)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.FK_idUtente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Vendite)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.FK_idUtente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vendite>()
                .HasMany(e => e.DettagliVendita)
                .WithRequired(e => e.Vendite)
                .HasForeignKey(e => e.FK_idVendita)
                .WillCascadeOnDelete(false);
        }
    }
}
