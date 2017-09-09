using System;

namespace Yoshi.QueryLayer.Model.Administration
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public Guid IdMerchant { get; set; }
        public string Name { get; set; }
        public string Implement { get; set; }
        public string Service { get; set; }
        public float Latitude { get; set; }
        public float longitude { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
