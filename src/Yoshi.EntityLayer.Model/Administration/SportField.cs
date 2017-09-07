using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Model.Base;

namespace Yoshi.EntityLayer.Model.Administration
{
    public class SportField : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdLocation { get; set; }
        public Location Location { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
