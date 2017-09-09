using System;
using System.ComponentModel.DataAnnotations;
using Yoshi.EntityLayer.Model.Base;

namespace Yoshi.EntityLayer.Model.Administration
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string ContactPhone { get; set; }
        
        [Required]
        public bool Active { get; set; }

        public IEquatable<Location> Locations { get; set; }
    }
}
