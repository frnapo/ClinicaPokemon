namespace ClinicaPokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visite")]
    public partial class Visite
    {
        [Key]
        public int idVisita { get; set; }

        public DateTime DataVisita { get; set; }

        [Required]
        public string EsameObiettivo { get; set; }

        [Required]
        public string DescrizioneCura { get; set; }

        public int FK_idAnimale { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
