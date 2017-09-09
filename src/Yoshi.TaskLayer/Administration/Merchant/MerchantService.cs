using System;
using System.Data.Entity.Core;
using Yoshi.EntityLayer.Model.Administration;
using Yoshi.EntityLayer.Service.Administration;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.TaskLayer.Administration
{
    public class MerchantService : IMerchantService
    {
        #region Services ---------------------
        private readonly IMerchantEntityService _merchantEntityService;
        #endregion        
        #region Constructor ------------------
        public MerchantService(IMerchantEntityService merchantEntityService)
        {
            this._merchantEntityService = merchantEntityService;
        }
        #endregion
        #region Methods ----------------------
        private Merchant CreateOrUpdate(MerchantBaseEvent @event, Merchant entity)
        {
            entity.Name = @event.Name;
            entity.BusinessName = @event.BusinessName;
            entity.TaxId = @event.TaxId;
            entity.Active = @event.Active;
            entity.ContactName = @event.ContactName;
            entity.ContactPhone = @event.ContactPhone;

            return entity;
        }
        #endregion
        #region IMerchantService
        public void Add(MerchantCreateEvent @event)
        {
            var entity = new Merchant();

            entity = this.CreateOrUpdate(@event, entity);

            entity.Id = Guid.NewGuid();
            entity.CreatedBy = @event.CreatedBy;
            entity.CreatedOn = DateTime.Now;

            this._merchantEntityService.Add(entity);
            this._merchantEntityService.Save();

            @event.Id = entity.Id;
        }

        public void Delete(MerchantDeleteEvent @event)
        {
            var entity = this._merchantEntityService.Get(@event.Id);

            if (entity == null)
                throw new ObjectNotFoundException();

            entity.IsDeleted = true;
            entity.DeletedBy = @event.DeletedBy;
            entity.DeletedOn = DateTime.Now;

            this._merchantEntityService.Edit(entity);
            this._merchantEntityService.Save();
        }

        public void Update(MerchantUpdateEvent @event)
        {
            var entity = this._merchantEntityService.Get(@event.Id);

            if (entity == null)
                throw new ObjectNotFoundException();

            entity = this.CreateOrUpdate(@event, entity);
            entity.ModifiedBy = @event.ModifiedBy;
            entity.ModifiedOn = DateTime.Now;

            this._merchantEntityService.Edit(entity);
            this._merchantEntityService.Save();
        }
        #endregion
    }
}
