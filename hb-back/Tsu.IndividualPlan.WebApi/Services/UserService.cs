using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;
using Tsu.IndividualPlan.WebApi.Models.Enum;
using Tsu.IndividualPlan.WebApi.Utils;

namespace Tsu.IndividualPlan.WebApi.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public UserService(IConfiguration configuration, IUserRepository repository)
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

        var user = new User(
            Password: PasswordUtils.GetPasswordHash(registrationDto.Password),
            Email: registrationDto.Email,
            Firstname: registrationDto.Firstname,
            Lastname: registrationDto.Lastname,
            Role: RoleUserEnum.User
        );
        var userAdded = await _repository.AddEntity(user);

        return userAdded.Role;
    }

    public async Task<User> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<ICollection<User>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Pagination<User>> Search(SearchDto searchDto)
    {
        return await _repository.Search(searchDto);
    }

    private string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("role", user.Role.ToString("D"))
        };
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}