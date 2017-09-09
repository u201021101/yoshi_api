using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoshi.TaskLayer.Model.Administration
{
    public abstract class LocationBaseEvent
    {
        public Guid Id { get; set; }
        public Guid IdMerchant { get; set; }
        public string Name { get; set; }
        public string Implement { get; set; }
        public string Service { get; set; }
        public float Latitude { get; set; }
        public float longitude { get; set; }
    }

    public class LocationCreateEvent : LocationBaseEvent
    {
        public Guid CreatedBy { get; set; }
    }

    public class LocationUpdateEvent : LocationBaseEvent
    {
        public Guid ModifiedBy { get; set; }
    }

    public class LocationDeleteEvent : LocationBaseEvent
    {
        public Guid DeletedBy { get; set; }
    }
}
