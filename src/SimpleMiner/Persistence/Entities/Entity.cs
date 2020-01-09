using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMiner.Persistence.Entities
{
    public class Entity
    {
        [Column(TypeName = "int")]
        public int Id { get; set; }
    }
}
