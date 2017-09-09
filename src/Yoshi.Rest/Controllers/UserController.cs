using AutoMapper;
using System;
using System.Data.Entity.Core;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Yoshi.Infrastructure.Rest.Base;
using Yoshi.Infrastructure.Rest.Model;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.QueryLayer.Administration.User;
using Yoshi.QueryLayer.Model.Administration;
using Yoshi.Rest.Model;
using Yoshi.Rest.Model.Administration;
using Yoshi.Rest.Names;
using Yoshi.TaskLayer.Administration;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.Rest.Controllers
{
    [RoutePrefix("api")]
    public class UserController : BaseApiController
    {
        #region Services ---------------------
        private readonly IUserService _UserService;
        private readonly IUserQueryService _UserQueryService;
        #endregion
        #region Constructor ------------------
        public UserController(IUserService UserService, IUserQueryService UserQueryService)
        {
            this._UserService = UserService;
            this._UserQueryService = UserQueryService;
        }
        #endregion
        #region Actions ----------------------
        [HttpGet]
        [Route("Users/search")]
        [ResponseType(typeof(RepresentationCollectionPaged<UserListRep>))]
        public IHttpActionResult Search([ModelBinder(typeof(ODataQueryModelBinder))] ODataQuery options)
        {
            var queryOptions = this.BuildQueryFilterOptions(options);

            var list = this._UserQueryService.Search(options.Filters, queryOptions);

            var collection = new RepresentationCollectionPaged<UserDto, UserListRep>(list);

            return this.Ok(collection);
        }

        [HttpGet]
        [Route("Users")]
        [ResponseType(typeof(RepresentationCollection<UserListRep>))]
        public IHttpActionResult List()
        {
            var list = this._UserQueryService.Find();

            var collection = new RepresentationCollection<UserDto, UserListRep>(list);

            return this.Ok(collection);
        }

        [HttpGet]
        [Route("Users/{id}", Name = UserResourceNames.Routes.GetById)]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var entity = this._UserQueryService.FindById(Guid.Parse(id));

                if (entity == null)
                    return this.NotFound();

                var representation = Mapper.Map<UserRep>(entity);

                return this.Ok(representation);
            }
            catch (FormatException)
            {
                return this.NotFound();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("Users", Name = UserResourceNames.Routes.PostCreate)]
        public IHttpActionResult Create(UserPostRep resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var @event = new UserCreateEvent
                {
                    Name = resource.Name,
                    BusinessName = resource.BusinessName,
                    TaxId = resource.TaxId,
                    ContactName = resource.ContactName,
                    ContactPhone = resource.ContactPhone,
                    CreatedBy = this.Identity
                };

                this._UserService.Add(@event);

                return this.CreatedAtRoute(UserResourceNames.Routes.GetById, new { id = @event.Id }, resource);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("Users/{id}", Name = UserResourceNames.Routes.PutUpdate)]
        public IHttpActionResult Update(string id, UserPostRep resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var @event = new UserUpdateEvent
                {
                    Id = Guid.Parse(id),
                    Name = resource.Name,
                    ModifiedBy = this.Identity,
                };

                this._UserService.Update(@event);

                return this.CreatedAtRoute(UserResourceNames.Routes.GetById, new { id = @event.Id }, resource);
            }
            catch (FormatException)
            {
                return this.BadRequest();
            }
            catch (ObjectNotFoundException)
            {
                return this.NotFound();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("Users/{id}", Name = UserResourceNames.Routes.Delete)]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var @event = new UserDeleteEvent
                {
                    Id = Guid.Parse(id),
                    DeletedBy = this.Identity,
                };

                this._UserService.Delete(@event);

                return this.Ok();
            }
            catch (FormatException)
            {
                return this.BadRequest();
            }
            catch (ObjectNotFoundException)
            {
                return this.NotFound();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
        #endregion
    }
}
