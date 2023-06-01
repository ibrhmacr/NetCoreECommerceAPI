namespace Application.DTOs.User;

public class ListUserDTO
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public string UserName { get; set; }
}

