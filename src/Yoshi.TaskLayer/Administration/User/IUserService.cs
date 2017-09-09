using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.TaskLayer.Model;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.TaskLayer.Administration
{
    public interface IUserService
    {
        void Add(UserCreateEvent @event);
        void Update(UserUpdateEvent @event);
        void Delete(UserDeleteEvent @event);
    }
}
