using System;
using System.ComponentModel.DataAnnotations;
using Yoshi.EntityLayer.Model.Base;

namespace Yoshi.EntityLayer.Model.Administration
{
    public class Merchant : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(12)]
        public string Ruc { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public Contact Contact { get; set; }

        public IEquatable<Location> Locations { get; set; }
    }
}
