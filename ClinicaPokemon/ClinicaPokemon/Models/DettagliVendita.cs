namespace ClinicaPokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliVendita")]
    public partial class DettagliVendita
    {
        [Key]
        public int idDettagli { get; set; }

        public int FK_idProdotto { get; set; }

        public int FK_idVendita { get; set; }

        public int Quantita { get; set; }

        public virtual Prodotti Prodotti { get; set; }

        public virtual Vendite Vendite { get; set; }
    }
}
