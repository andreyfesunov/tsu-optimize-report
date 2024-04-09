using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.IdentityModel.Tokens;
using StudentHubBackend.Exceptions;
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
        private readonly IMapper _mapper;

        public UserService(
            IConfiguration configuration,
            UserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserById(int userId)
        {
            var user = await _userRepository.GetById(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserLoginDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (GetPasswordHash(loginDto.Password) == user.Password)
            {

                var userLogin = new UserLoginDto();
                userLogin.Token = GenerateJWT(_mapper.Map<UserDto>(user));
                return userLogin;
            }

            throw new Exception("Nickname or Password are not correct");
        }

        public async Task<bool> Registrate(RegistrationDto registrationDto)
        {
            var userExistsCheck = await _userRepository.GetUserByEmail(registrationDto.Email);
            if (userExistsCheck != null)
            {
                throw new UserAlreadyExistsException();
            }

            if (registrationDto.Password != registrationDto.ConfirmPassword)
            {
                throw new PasswordNotConfirmedException();
            }

            var user = _mapper.Map<User>(registrationDto);
            user.Id = new Guid();
            user.Password = GetPasswordHash(registrationDto.Password);

            var userAdded = await _userRepository.AddEntity(user);
            return userAdded != null;
        }

        private string GenerateJWT(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                //new Claim(ClaimTypes.NameIdentifier,user.Nickname),
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
