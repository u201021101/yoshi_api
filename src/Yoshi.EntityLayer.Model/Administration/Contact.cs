using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Model.Base;

namespace Yoshi.EntityLayer.Model.Administration
{
    public class Contact : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ContacName { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }
    }
}
