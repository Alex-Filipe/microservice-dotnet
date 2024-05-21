using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroservice.Models

{
    public abstract class BaseEntity
    {
        [Column("created_at", TypeName = "TIMESTAMP")]
        public DateTime? Created_at { get; set; }

        [Column("updated_at", TypeName = "TIMESTAMP")]
        public DateTime? Updated_at { get; set; }
    }

}