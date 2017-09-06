using System;

namespace Yoshi.TaskLayer.Model.Administration
{
    public abstract class MerchantBaseEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class MerchantCreateEvent : MerchantBaseEvent
    {
        public Guid CreatedBy { get; set; }
    }

    public class MerchantUpdateEvent : MerchantBaseEvent
    {
        public Guid ModifiedBy { get; set; }
    }

    public class MerchantDeleteEvent : MerchantBaseEvent
    {
        public Guid DeletedBy { get; set; }
    }
}
