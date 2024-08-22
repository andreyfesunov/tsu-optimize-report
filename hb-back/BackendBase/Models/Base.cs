﻿using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Base
    {
        public Base(Guid? Id = null)
        {
            this.Id = Id ?? Guid.NewGuid();
        }

        [Required]
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
