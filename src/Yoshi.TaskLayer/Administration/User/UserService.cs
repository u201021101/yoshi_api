using System;
using System.Data.Entity.Core;
using Yoshi.EntityLayer.Model.Administration;
using Yoshi.EntityLayer.Service.Administration;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.TaskLayer.Administration
{
    public class UserService : IUserService
    {
        #region Services ---------------------
        private readonly IUserEntityService _UserEntityService;
        #endregion        
        #region Constructor ------------------
        public UserService(IUserEntityService UserEntityService)
        {
            this._UserEntityService = UserEntityService;
        }
        #endregion
        #region Methods ----------------------
        private User CreateOrUpdate(UserBaseEvent @event, User entity)
        {
            entity.Name = @event.Name;
            entity.Lastname = @event.Lastname;
            entity.Email = @event.email;
            entity.Active = @event.Active;
            entity.ContactName = @event.ContactName;
            entity.ContactPhone = @event.ContactPhone;

            return entity;
        }
        #endregion
        #region IUserService
        public void Add(UserCreateEvent @event)
        {
            var entity = new User();

            entity = this.CreateOrUpdate(@event, entity);

            entity.Id = Guid.NewGuid();
            entity.CreatedBy = @event.CreatedBy;
            entity.CreatedOn = DateTime.Now;

            this._UserEntityService.Add(entity);
            this._UserEntityService.Save();

            @event.Id = entity.Id;
        }

        public void Delete(UserDeleteEvent @event)
        {
            var entity = this._UserEntityService.Get(@event.Id);

            if (entity == null)
                throw new ObjectNotFoundException();

            entity.IsDeleted = true;
            entity.DeletedBy = @event.DeletedBy;
            entity.DeletedOn = DateTime.Now;

            this._UserEntityService.Edit(entity);
            this._UserEntityService.Save();
        }

        public void Update(UserUpdateEvent @event)
        {
            var entity = this._UserEntityService.Get(@event.Id);

            if (entity == null)
                throw new ObjectNotFoundException();

            entity = this.CreateOrUpdate(@event, entity);
            entity.ModifiedBy = @event.ModifiedBy;
            entity.ModifiedOn = DateTime.Now;

            this._UserEntityService.Edit(entity);
            this._UserEntityService.Save();
        }
        #endregion
    }
}
