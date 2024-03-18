namespace ClinicaPokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vendite")]
    public partial class Vendite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendite()
        {
            DettagliVendita = new HashSet<DettagliVendita>();
        }

        [Key]
        public int idVendita { get; set; }

        public int FK_idUtente { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataVendita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliVendita> DettagliVendita { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
