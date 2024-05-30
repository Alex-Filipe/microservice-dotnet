using UserMicroservice.src.Models;

namespace UserMicroserice.src.Dtos.RoleDTOs
{
    public class AllRolesDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required int User_id { get; set; }
    }
}
