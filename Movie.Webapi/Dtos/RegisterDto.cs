namespace Movie.Webapi.Dtos
{
    public class RegisterDto:LoginDto
    {
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
