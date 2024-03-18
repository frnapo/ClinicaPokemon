namespace ClinicaPokemon.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ricoveri")]
    public partial class Ricoveri
    {
        [Key]
        public int idRicovero { get; set; }

        public int FK_idAnimale { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInizioRicovero { get; set; }

        [Required]
        public string FotoAnimale { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrezzoRicovero { get; set; }

        public bool? Attivo { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
