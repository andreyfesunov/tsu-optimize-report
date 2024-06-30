using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Models.Enum;
using BackendBase.Repositories;
using BackendBase.Utils;
using Microsoft.IdentityModel.Tokens;

namespace BackendBase.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserRepository _userRepository;
    protected MappingHelper<User, UserDto> _mappingHelper;
    protected IBaseRepository<User> _repository;

    public async Task<User> AddEntity(User entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<UserDto> GetById(Guid id)
    {
        return _mappingHelper.toDto(await _repository.GetById(id));
    }

    public async Task<ICollection<UserDto>> GetAll()
    {
        return _mappingHelper.toDto(await _repository.GetAll());
    }

    public async Task<User> Update(User entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<PaginationDto<UserDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }

    public UserService(
        IConfiguration configuration,
        UserRepository userRepository,
        IMapper mapper,
        UserRepository repository
    )
    {
        _userRepository = userRepository;
        _repository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
        _repository = repository;
        _mappingHelper = new MappingHelper<User, UserDto>(_mapper);
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
            Firstname = registrationDto.Firstname,
            Lastname = registrationDto.Lastname,
            Role = RoleUserEnum.User
        };
        var userAdded = await _userRepository.AddEntity(user);

        return userAdded.Role;
    }

    private string GenerateJwt(UserDto user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", user.Id),
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