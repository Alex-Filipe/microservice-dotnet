using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome não pode ter mais do que 255 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo email não pode ter mais do que 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo email deve conter um endereço de e-mail válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Senha não pode ter mais do que 255 caracteres.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "O campo phone é obrigatório.")]
        public required string Phone { get; set; }
    }
}
