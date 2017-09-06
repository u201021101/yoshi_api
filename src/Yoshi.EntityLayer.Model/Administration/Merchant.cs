﻿using System;
using System.ComponentModel.DataAnnotations;
using Yoshi.EntityLayer.Model.Base;

namespace Yoshi.EntityLayer.Model.Administration
{
    public class Merchant: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
