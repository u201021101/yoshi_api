using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Model.Base;

namespace Yoshi.EntityLayer.Model.Administration
{
    public class Location : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? IdMerchant { get; set; }
        public Merchant Merchant { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Implement { get; set; }

        [Required]
        [StringLength(200)]
        public string Service { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float longitude { get; set; }

        public IEnumerable<SportField> SportsFields { get; set; }
    }
}
