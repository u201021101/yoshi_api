using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Service.Administration;
using Yoshi.TaskLayer.Model.Administration;
using Yoshi.EntityLayer.Model.Administration;
using System.Data.Entity.Core;

namespace Yoshi.TaskLayer.Administration
{
    public class LocationService : ILocationService
    {
        #region Services ---------------------
        private readonly ILocationEntityService _locationEntityService;
        #endregion
        #region Constructor ------------------
        public LocationService(ILocationEntityService locationEntityService)
        {
            this._locationEntityService = locationEntityService;
        }

        #endregion
        #region Methods ----------------------
        private Location CreateOrUpdate(LocationBaseEvent @event, Location entity)
        {
            entity.Name = @event.Name;
            entity.IdMerchant = @event.IdMerchant;
            entity.Implement = @event.Implement;
            entity.Service = @event.Service;
            entity.Latitude = @event.Latitude;
            entity.longitude = @event.longitude;

            return entity;
        }
        #endregion

        #region ILocationService ----------------------
        public void Add(LocationCreateEvent @event)
        {
            var entity = new Location();

            entity = this.CreateOrUpdate(@event, entity);

            entity.Id = Guid.NewGuid();
            entity.CreatedBy = @event.CreatedBy;
            entity.CreatedOn = DateTime.Now;

            this._locationEntityService.Add(entity);
            this._locationEntityService.Save();

            @event.Id = entity.Id;
        }

        public void Delete(LocationDeleteEvent @event)
        {
            var entity = this._locationEntityService.Get(@event.Id);

            if (entity == null)
                throw new ObjectNotFoundException();

            entity.IsDeleted = true;
            entity.DeletedBy = @event.DeletedBy;
            entity.DeletedOn = DateTime.Now;

            this._locationEntityService.Edit(entity);
            this._locationEntityService.Save();
        }

        public void Update(LocationUpdateEvent @event)
        {
            var entity = this._locationEntityService.Get(@event.Id);

            if (entity == null)
                throw new ObjectNotFoundException();

            entity = this.CreateOrUpdate(@event, entity);
            entity.ModifiedBy = @event.ModifiedBy;
            entity.ModifiedOn = DateTime.Now;

            this._locationEntityService.Edit(entity);
            this._locationEntityService.Save();
        }
        #endregion
    }
}
