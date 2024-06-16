﻿using System.ComponentModel.DataAnnotations;

namespace BackendBase.Dto;

public class RegistrationDto
{
    [Required] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required] public string Firstname { get; set; }

    [Required] public string Lastname { get; set; }
}