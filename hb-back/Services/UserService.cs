﻿using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendBase.Utils;
using BackendBase.Models.Enum;

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

        public async Task<UserDto> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserLoginDto> LogIn(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Not found user with given email");

            if (PasswordUtils.GetPasswordHash(loginDto.Password) != user.Password)
                throw new UnauthorizedAccessException("Wrong password");

            var userLogin = new UserLoginDto
            {
                Token = GenerateJwt(_mapper.Map<UserDto>(user))
            };
            return userLogin;
        }

        public async Task<RoleUserEnum> Reg(RegistrationDto registrationDto)
        {
            var userExistsCheck = await _userRepository.GetUserByEmail(registrationDto.Email);

            if (userExistsCheck != null)
                throw new UnauthorizedAccessException();

            var user = new User
            {
                Id = new Guid(),
                Password = PasswordUtils.GetPasswordHash(registrationDto.Password),
                Email = registrationDto.Email,
                Firstname = "",
                Lastname = "",
                RoleUsers = RoleUserEnum.User,
            };
            var userAdded = await _userRepository.AddEntity(user);

            return userAdded.RoleUsers;
        }

        private string GenerateJwt(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
