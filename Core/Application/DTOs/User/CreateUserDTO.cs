namespace Application.DTOs.User;

public class CreateUserDTO
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PasswordConfirm { get; set; }
}