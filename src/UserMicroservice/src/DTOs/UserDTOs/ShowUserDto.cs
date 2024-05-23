namespace UserMicroserice.src.Dtos.UserDTOs
{
    public class ShowUserDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateBirth { get; set; }
    }
}
