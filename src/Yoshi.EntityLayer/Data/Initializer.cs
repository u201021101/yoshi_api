using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Context;

namespace Yoshi.EntityLayer.Data
{
    internal class Initializer
    {
        public static void Register(ApplicationDbContext context)
        {
            RoleInitializer.Register(context);
            UserInitializer.Register(context);
            ClientApplicationInitializer.Register(context);
        }
    }
}
