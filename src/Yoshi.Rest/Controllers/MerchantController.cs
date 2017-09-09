using AutoMapper;
using System;
using System.Data.Entity.Core;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Yoshi.Infrastructure.Rest.Base;
using Yoshi.Infrastructure.Rest.Model;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.QueryLayer.Administration.Merchant;
using Yoshi.QueryLayer.Model.Administration;
using Yoshi.Rest.Model;
using Yoshi.Rest.Model.Administration;
using Yoshi.Rest.Names;
using Yoshi.TaskLayer.Administration;
using Yoshi.TaskLayer.Model.Administration;

namespace Yoshi.Rest.Controllers
{
    [RoutePrefix("api")]
    public class MerchantController : BaseApiController
    {
        #region Services ---------------------
        private readonly IMerchantService _merchantService;
        private readonly IMerchantQueryService _merchantQueryService;
        #endregion
        #region Constructor ------------------
        public MerchantController(IMerchantService merchantService, IMerchantQueryService merchantQueryService)
        {
            this._merchantService = merchantService;
            this._merchantQueryService = merchantQueryService;
        }
        #endregion
        #region Actions ----------------------
        [HttpGet]
        [Route("merchants/search")]
        [ResponseType(typeof(RepresentationCollectionPaged<MerchantListRep>))]
        public IHttpActionResult Search([ModelBinder(typeof(ODataQueryModelBinder))] ODataQuery options)
        {
            var queryOptions = this.BuildQueryFilterOptions(options);

            var list = this._merchantQueryService.Search(options.Filters, queryOptions);

            var collection = new RepresentationCollectionPaged<MerchantDto, MerchantListRep>(list);

            return this.Ok(collection);
        }

        [HttpGet]
        [Route("merchants")]
        [ResponseType(typeof(RepresentationCollection<MerchantListRep>))]
        public IHttpActionResult List()
        {
            var list = this._merchantQueryService.Find();

            var collection = new RepresentationCollection<MerchantDto, MerchantListRep>(list);

            return this.Ok(collection);
        }

        [HttpGet]
        [Route("merchants/{id}", Name = MerchantResourceNames.Routes.GetById)]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var entity = this._merchantQueryService.FindById(Guid.Parse(id));

                if (entity == null)
                    return this.NotFound();

                var representation = Mapper.Map<MerchantRep>(entity);

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
        [Route("merchants", Name = MerchantResourceNames.Routes.PostCreate)]
        public IHttpActionResult Create(MerchantPostRep resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var @event = new MerchantCreateEvent
                {
                    Name = resource.Name,
                    BusinessName = resource.BusinessName,
                    TaxId = resource.TaxId,
                    ContactName = resource.ContactName,
                    ContactPhone = resource.ContactPhone,
                    CreatedBy = this.Identity
                };

                this._merchantService.Add(@event);

                return this.CreatedAtRoute(MerchantResourceNames.Routes.GetById, new { id = @event.Id }, resource);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("merchants/{id}", Name = MerchantResourceNames.Routes.PutUpdate)]
        public IHttpActionResult Update(string id, MerchantPostRep resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var @event = new MerchantUpdateEvent
                {
                    Id = Guid.Parse(id),
                    Name = resource.Name,
                    ModifiedBy = this.Identity,
                };

                this._merchantService.Update(@event);

                return this.CreatedAtRoute(MerchantResourceNames.Routes.GetById, new { id = @event.Id }, resource);
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
        [Route("merchants/{id}", Name = MerchantResourceNames.Routes.Delete)]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var @event = new MerchantDeleteEvent
                {
                    Id = Guid.Parse(id),
                    DeletedBy = this.Identity,
                };

                this._merchantService.Delete(@event);

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
