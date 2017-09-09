using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Service.Administration;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.Infrastructure.Rest.PagedList;
using Yoshi.Infrastructure.Rest.Query;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Administration.Location
{
    public class LocationQueryService: ILocationQueryService
    {
        #region Services ---------------------
        private readonly ILocationEntityService locationEntityService;
        private readonly Func<ApplicationDbContext> _dbContext;
        #endregion
        #region Constructor ------------------

        public LocationQueryService(ILocationEntityService locationEntityService, Func<ApplicationDbContext> dbContext)
        {
            this.locationEntityService = locationEntityService;
            this._dbContext = dbContext;
        }
        #endregion

        #region ILocationQueryService ----------------------

        public IEnumerable<LocationDto> Find()
        {
            var list = this.locationEntityService.FindAll().Where(o => !o.IsDeleted);
            return Mapper.Map<IEnumerable<LocationDto>>(list);
        }

        public LocationDto FindById(Guid id)
        {
            var entity = this.locationEntityService.Get(id);

            if (entity.IsDeleted) return null;
            else return Mapper.Map<LocationDto>(entity);
        }

        public IPagedList<LocationDto> Search(Filter[] filters, QueryFilterOptions queryOptions = null)
        {
            using (var context = this._dbContext())
            {
                var query = from o in context.Locations
                            where !o.IsDeleted
                            select new LocationDto
                            {
                                Id = o.Id,
                                Name = o.Name,
                                CreatedOn = o.CreatedOn
                            };

                foreach (var item in filters)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        switch (item.Attribute.ToLower())
                        {
                            case "name":
                                switch (item.Operator)
                                {
                                    case FilterOperator.lk:
                                        query = query.Where(o => o.Name.Contains(item.Value));
                                        break;
                                    case FilterOperator.eq:
                                        query = query.Where(o => o.Name.Equals(item.Value));
                                        break;
                                    case FilterOperator.ne:
                                        query = query.Where(o => !o.Name.Equals(item.Value));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

                queryOptions.OrderByOptions.Name = queryOptions.OrderByOptions.Name ?? "CreatedOn";
                query = PagedListUtil.BuildOrderExpression<LocationDto, DateTime>(query, queryOptions.OrderByOptions.Name, queryOptions.OrderByOptions.Ascending);
                var queryPaged = query.ToPagedList(queryOptions.PagingOptions.PageNumber, queryOptions.PagingOptions.PageSize);

                return queryPaged;
            }
        }
        #endregion

    }
}
