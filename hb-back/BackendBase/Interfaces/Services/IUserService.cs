﻿using BackendBase.Dto;
using BackendBase.Models;
using BackendBase.Models.Enum;

namespace BackendBase.Interfaces.Services;

public interface IUserService
{
    Task<string> LogIn(LoginDto loginDto);
    Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
    Task<User> GetById(Guid id);
    Task<ICollection<User>> GetAll();
    Task<Pagination<User>> Search(SearchDto searchDto);
}