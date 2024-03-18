namespace ClinicaPokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Animali")]
    public partial class Animali
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animali()
        {
            Ricoveri = new HashSet<Ricoveri>();
            Visite = new HashSet<Visite>();
        }

        [Key]
        public int idAnimale { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Tipologia { get; set; }

        [Required]
        public string Colore { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataNascita { get; set; }

        public bool Microchip { get; set; }

        public string NrMicro { get; set; }

        public int FK_idUtente { get; set; }

        public virtual Utenti Utenti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ricoveri> Ricoveri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visite> Visite { get; set; }
    }
}
