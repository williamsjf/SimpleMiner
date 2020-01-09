using SimpleMiner.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMiner.Models
{
    public abstract class Input : Entity
    {
        [Column("Status", TypeName = "varchar(20)")]
        public string Status { get; set; }

        [Column("UltimaConsulta", TypeName = "datetime2")]
        public DateTime? UltimaConsulta { get; set; }

        [Column("LoteId", TypeName = "int")]
        [ForeignKey("Lote")]
        public virtual int LoteId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public virtual TimeSpan? TempoConsulta { get; set; }

        [Column(TypeName = "varchar(500)")]
        public virtual string Mensagem { get; set; }
    }
}
