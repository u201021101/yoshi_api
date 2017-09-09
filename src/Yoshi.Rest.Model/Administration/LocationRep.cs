using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoshi.Rest.Model.Administration
{
    public abstract class BaseLocationRep
    {
        [Required]
        public Guid IdMerchant { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Implement { get; set; }
        [Required]
        public string Service { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float longitude { get; set; }
    }

    public class LocationRep : BaseLocationRep
    {
        public Guid Id { get; set; }
    }
    public class LocationListRep : BaseLocationRep
    {
        public Guid Id { get; set; }
    }
    public class LocationPostRep : BaseLocationRep
    {

    }
}
