using SimpleMiner.Persistence.Entities;
using System;

namespace SimpleMiner.Models
{
    public class Lote : Entity
    {
        public Lote()
        {
        }

        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public bool Ativo { get; set; }
    }
}
