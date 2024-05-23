using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroserice.Dtos.UserDTOs
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        [Column("nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome não pode ter mais do que 255 caracteres.")]
        public required string Name { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Email não pode ter mais do que 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de e-mail válido.")]
        public required string Email { get; set; }

        [Column("phone")]
        [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
        public required DateOnly DateBirth { get; set; }
    }
}
