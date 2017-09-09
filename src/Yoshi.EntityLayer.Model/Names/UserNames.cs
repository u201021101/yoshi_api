using System;

namespace Yoshi.EntityLayer.Model.Names
{
    public class UserNames
    {
        public const string System = "system";
        public static Guid SystemId = new Guid("00000000-0000-0000-0000-000000000000");

        public const string AppMerchant = "admin_merchant";
        public static Guid AppMerchantId = new Guid("00000000-0000-0000-0000-000000000001");

        public const string AppCustomer = "admin_customer";
        public static Guid AppCustomerId = new Guid("00000000-0000-0000-0000-000000000002");
    }
}
