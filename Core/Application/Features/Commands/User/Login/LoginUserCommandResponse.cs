using Application.DTOs;

namespace Application.Features.Commands.User.Login;

public class LoginUserCommandResponse
{
    
}

public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
{
    public Token Token { get; set; }
}

public class LoginUserErrorCommandResponse : LoginUserCommandResponse
{
    public string Message { get; set; }
}