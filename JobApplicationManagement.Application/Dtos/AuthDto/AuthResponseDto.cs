namespace JobApplicationManagement.Application.Dtos.AuthDto
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}