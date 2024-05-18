using System.ComponentModel.DataAnnotations;

namespace EmailService.DTOs
{
    public class WelcomeEmailDto
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Email não pode ter mais do que 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de e-mail válido.")]
        public required string Email { get; set; }
    }
}