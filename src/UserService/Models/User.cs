using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    [Table("usuarios")]
    public class User : BaseEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("password")]
        public required string Password { get; set; }

        [Column("phone")]
        public required string Phone { get; set; }
    }
}
