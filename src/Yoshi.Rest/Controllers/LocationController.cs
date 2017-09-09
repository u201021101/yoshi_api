using AutoMapper;
using System;
using System.Data.Entity.Core;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Yoshi.Infrastructure.Rest.Base;
using Yoshi.Infrastructure.Rest.Model;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.QueryLayer.Administration.Location;
using Yoshi.QueryLayer.Model.Administration;
using Yoshi.Rest.Model.Administration;
using Yoshi.Rest.Names;
using Yoshi.TaskLayer.Administration;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.Rest.Controllers
{
    [RoutePrefix("api")]
    public class LocationController : BaseApiController
    {
        #region Services ---------------------
        private readonly ILocationQueryService _locationQueryService;
        private readonly ILocationService _locationService;
        #endregion
        #region Constructor ------------------
        public LocationController(ILocationQueryService locationQueryService
                                , ILocationService locationService)
        {
            this._locationQueryService = locationQueryService;
            this._locationService = locationService;
        }
        #endregion
        #region Actions ----------------------
        [HttpGet]
        [Route("locations/search", Name = LocationResourceNames.Routes.GetQuery)]
        [ResponseType(typeof(RepresentationCollectionPaged<LocationListRep>))]
        public IHttpActionResult Search([ModelBinder(typeof(ODataQueryModelBinder))] ODataQuery options)
        {
            var queryOptions = this.BuildQueryFilterOptions(options);

            var list = this._locationQueryService.Search(options.Filters, queryOptions);

            var collection = new RepresentationCollectionPaged<LocationDto, LocationListRep>(list);

            return this.Ok(collection);
        }

        [HttpGet]
        [Route("locations")]
        [ResponseType(typeof(RepresentationCollection<LocationListRep>))]
        public IHttpActionResult List()
        {
            var list = this._locationQueryService.Find();

            var collection = new RepresentationCollection<LocationDto, LocationListRep>(list);

            return this.Ok(collection);
        }

        [HttpGet]
        [Route("locations/{id}", Name = LocationResourceNames.Routes.GetById)]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var entity = this._locationQueryService.FindById(Guid.Parse(id));

                if (entity == null)
                    return this.NotFound();

                var representation = Mapper.Map<LocationRep>(entity);

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
        [Route("locations", Name = LocationResourceNames.Routes.PostCreate)]
        public IHttpActionResult Create(LocationPostRep resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var @event = new LocationCreateEvent
                {
                    Name = resource.Name,
                    IdMerchant = resource.IdMerchant,
                    Implement = resource.Implement,
                    Service = resource.Service,
                    Latitude = resource.Latitude,
                    longitude = resource.longitude,
                    
                    CreatedBy = this.Identity
                };

                this._locationService.Add(@event);

                return this.CreatedAtRoute(LocationResourceNames.Routes.GetById, new { id = @event.Id }, resource);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("locations/{id}", Name = LocationResourceNames.Routes.PutUpdate)]
        public IHttpActionResult Update(string id, LocationPostRep resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var @event = new LocationUpdateEvent
                {
                    Id = Guid.Parse(id),
                    Name = resource.Name,
                    ModifiedBy = this.Identity,
                };

                this._locationService.Update(@event);

                return this.CreatedAtRoute(LocationResourceNames.Routes.GetById, new { id = @event.Id }, resource);
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
        [Route("locations/{id}", Name = LocationResourceNames.Routes.Delete)]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var @event = new LocationDeleteEvent
                {
                    Id = Guid.Parse(id),
                    DeletedBy = this.Identity,
                };

                this._locationService.Delete(@event);

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
