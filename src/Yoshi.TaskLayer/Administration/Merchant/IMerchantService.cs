using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.TaskLayer.Model;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.TaskLayer.Administration
{
    public interface IMerchantService
    {
        void Add(MerchantCreateEvent @event);
        void Update(MerchantUpdateEvent @event);
        void Delete(MerchantDeleteEvent @event);
    }
}
