using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Models.Enum;
using BackendBase.Utils;
using Microsoft.IdentityModel.Tokens;

namespace BackendBase.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public UserService(
        IConfiguration configuration,
        IUserRepository repository
    )
    {
        _configuration = configuration;
        _repository = repository;
    }

    public async Task<string> LogIn(LoginDto loginDto)
    {
        var user = await _repository.GetByEmail(loginDto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Not found user with given email");

        if (PasswordUtils.GetPasswordHash(loginDto.Password) != user.Password)
            throw new UnauthorizedAccessException("Wrong password");

        return GenerateJwt(user);
    }

    public async Task<RoleUserEnum> Reg(RegistrationDto registrationDto)
    {
        var userExistsCheck = await _repository.GetByEmail(registrationDto.Email);

        if (userExistsCheck != null)
            throw new UnauthorizedAccessException();

        var user = new User
        {
            Id = new Guid(),
            Password = PasswordUtils.GetPasswordHash(registrationDto.Password),
            Email = registrationDto.Email,
            Firstname = registrationDto.Firstname,
            Lastname = registrationDto.Lastname,
            Role = RoleUserEnum.User
        };
        var userAdded = await _repository.AddEntity(user);

        return userAdded.Role;
    }

    private string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("role", user.Role.ToString("D"))
        };
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}