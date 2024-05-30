using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroservice.src.Models
{
    [Table("user_roles")]
    public class Role : BaseEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("user_id")]
        public required int User_id { get; set; }
    }
}
