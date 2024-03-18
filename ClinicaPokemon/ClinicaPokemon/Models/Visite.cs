namespace ClinicaPokemon.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Visite")]
    public partial class Visite
    {
        [Key]
        public int idVisita { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataVisita { get; set; }

        [Required]
        public string EsameObiettivo { get; set; }

        [Required]
        public string DescrizioneCura { get; set; }

        public int FK_idAnimale { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
