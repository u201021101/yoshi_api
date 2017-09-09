using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.TaskLayer.Administration
{
    public interface ILocationService
    {
        void Add(LocationCreateEvent @event);
        void Update(LocationUpdateEvent @event);
        void Delete(LocationDeleteEvent @event);
    }
}
