using SimpleMiner.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMiner.Models
{
    public class Output : Entity
    {
        public Output()
        {
            DataConsulta = DateTime.Now;
            Status = "Unhandled";
        }

        [NotMapped]
        public string Status { get; set; }

        [Column(TypeName = "int")]
        [ForeignKey("Lote")]
        public int LoteId { get; set; }

        [Column(TypeName = "int")]
        public int? InputId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataConsulta { get; set; }
    }
}
