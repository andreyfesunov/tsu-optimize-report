using BackendBase.Data.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendBase.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetById(string id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) { throw new Exception("User not found"); }

            return user;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetByNickname(loginDto.Nickname);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            if (GetPasswordHash(loginDto.Password) == user.Password)
            {

                var tokendDto = new TokenDto();
                tokendDto.Id = user.Id;
                tokendDto.Token = GenerateJWT(user);
                return tokendDto;
            }

            throw new Exception("Nickname or Password are not correct");
        }

        public async Task<bool> Registrate(RegistrationDto registrationDto)
        {
            var userExistsCheck = await _userRepository.GetByNickname(registrationDto.Nickname);
            if (userExistsCheck != null)
            {
                throw new Exception("User already exists");
            }

            if (registrationDto.Password != registrationDto.ConfirmPassword)
            {
                throw new Exception("Passwords are not the same");
            }

            var user = new User
            {
                Nickname = registrationDto.Nickname,
                Email = registrationDto.Email,
            };

            user.Password = GetPasswordHash(registrationDto.Password);
            await _userRepository.AddEntity(user);
            return true;
        }

        private string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,user.Nickname),
                 new Claim(ClaimTypes.Email,user.Email)
             };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetPasswordHash(string password)
        {
            var sha = SHA256.Create();
            var byteArray = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(byteArray);
            return Convert.ToBase64String(hash);
        }
    }
}
