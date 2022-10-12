namespace Chat.Web.Models;

public class RegisterRequest: LoginRequest
{
    public string Email { get; set; } = null!;
    public string RetypePassword { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}