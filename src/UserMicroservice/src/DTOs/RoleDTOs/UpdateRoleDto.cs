using System.ComponentModel.DataAnnotations;

namespace UserMicroserice.src.Dtos.RoleDTOs
{
    public class UpdateRoleDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome não pode ter mais do que 255 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O campo user_id é obrigatório.")]
        public required int User_id { get; set; }
    }
}
