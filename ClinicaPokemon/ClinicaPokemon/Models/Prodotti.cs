namespace ClinicaPokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            DettagliVendita = new HashSet<DettagliVendita>();
        }

        [Key]
        public int idProdotto { get; set; }

        [Required]
        public string NomeProdotto { get; set; }

        public bool Tipo { get; set; }

        public int FK_idUsoProdotto { get; set; }

        public int FK_idDittaFornitrice { get; set; }

        public int FK_idCassetto { get; set; }

        public int FK_idArmadietto { get; set; }

        public virtual Armadietti Armadietti { get; set; }

        public virtual Cassetti Cassetti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliVendita> DettagliVendita { get; set; }

        public virtual DittaFornitrice DittaFornitrice { get; set; }

        public virtual UsoProdotti UsoProdotti { get; set; }
    }
}
