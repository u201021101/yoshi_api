using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Service.Administration;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.Infrastructure.Rest.PagedList;
using Yoshi.Infrastructure.Rest.Query;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Administration.Merchant
{
    public class MerchantQueryService : IMerchantQueryService
    {
        #region EntityServices ---------------
        private readonly IMerchantEntityService _merchantEntityService;
        private readonly Func<ApplicationDbContext> _dbContext;
        #endregion 
        #region Constructor ------------------
        public MerchantQueryService(IMerchantEntityService merchantEntityService, Func<ApplicationDbContext> dbContext)
        {
            this._merchantEntityService = merchantEntityService;
            this._dbContext = dbContext;
        }
        #endregion
        #region IMerchantQueryService        
        public IEnumerable<MerchantDto> Find()
        {
            var list = this._merchantEntityService.FindAll().Where(o => !o.IsDeleted);
            return Mapper.Map<IEnumerable<MerchantDto>>(list);
        }

        public MerchantDto FindById(Guid id)
        {
            var entity = this._merchantEntityService.Get(id);

            if (entity.IsDeleted) return null;
            else return Mapper.Map<MerchantDto>(entity);
        }

        public IPagedList<MerchantDto> Search(Filter[] filters, QueryFilterOptions queryOptions = null)
        {
            using (var context = this._dbContext())
            {
                var query = from o in context.Merchants
                            where !o.IsDeleted
                            select new MerchantDto
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
                query = PagedListUtil.BuildOrderExpression<MerchantDto, DateTime>(query, queryOptions.OrderByOptions.Name, queryOptions.OrderByOptions.Ascending);
                var queryPaged = query.ToPagedList(queryOptions.PagingOptions.PageNumber, queryOptions.PagingOptions.PageSize);

                return queryPaged;
            }
        }
        #endregion
    }
}
