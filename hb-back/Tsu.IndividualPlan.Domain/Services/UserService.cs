using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tsu.IndividualPlan.Domain.Dto.Auth;
using Tsu.IndividualPlan.Domain.Enumerations;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.Domain.Utils;

namespace Tsu.IndividualPlan.Domain.Services;

public class UserService(IConfiguration configuration, IUserRepository repository) : IUserService
{
    public async Task<string> LogIn(LoginDto loginDto)
    {
        var user = await repository.GetByEmail(loginDto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Not found user with given email");

        if (PasswordUtils.GetPasswordHash(loginDto.Password) != user.Password)
            throw new UnauthorizedAccessException("Wrong password");

        return GenerateJwt(user);
    }

    public async Task<RoleUserEnum> Reg(RegistrationDto registrationDto)
    {
        var userExistsCheck = await repository.GetByEmail(registrationDto.Email);

        if (userExistsCheck != null)
            throw new UnauthorizedAccessException();

        var user = new User(
            Password: PasswordUtils.GetPasswordHash(registrationDto.Password),
            Email: registrationDto.Email,
            Firstname: registrationDto.Firstname,
            Lastname: registrationDto.Lastname,
            Role: RoleUserEnum.User
        );
        var userAdded = await repository.AddEntity(user);

        return userAdded.Role;
    }

    public async Task<User> GetById(Guid id)
    {
        return await repository.GetById(id);
    }

    public async Task<ICollection<User>> GetAll()
    {
        return await repository.GetAll();
    }

    public async Task<Pagination<User>> Search(Search search)
    {
        return await repository.Search(search);
    }

    private string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("role", user.Role.ToString("D"))
        };
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}