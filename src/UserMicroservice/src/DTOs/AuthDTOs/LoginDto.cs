using System.ComponentModel.DataAnnotations;

namespace UserMicroserice.src.Dtos.AuthDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Email não pode ter mais do que 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de e-mail válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public required string Password { get; set; }
    }
}
