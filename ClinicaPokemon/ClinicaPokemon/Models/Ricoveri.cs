namespace ClinicaPokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ricoveri")]
    public partial class Ricoveri
    {
        [Key]
        public int idRicovero { get; set; }

        public int FK_idAnimale { get; set; }

        public DateTime DataInizioRicovero { get; set; }

        [Required]
        public string FotoAnimale { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrezzoRicovero { get; set; }

        public bool? Attivo { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
