﻿using System.ComponentModel.DataAnnotations;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class Base
{
    public Base(Guid? Id = null)
    {
        this.Id = Id ?? Guid.NewGuid();
    }

    [Required] public Guid Id { get; private set; } = Guid.NewGuid();
}