using System;

namespace Yoshi.TaskLayer.Model.Administration
{
    public abstract class UserBaseEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
    }

    public class UserCreateEvent : UserBaseEvent
    {
        public Guid CreatedBy { get; set; }
    }

    public class UserUpdateEvent : UserBaseEvent
    {
        public Guid ModifiedBy { get; set; }
    }

    public class UserDeleteEvent : UserBaseEvent
    {
        public Guid DeletedBy { get; set; }
    }
}
