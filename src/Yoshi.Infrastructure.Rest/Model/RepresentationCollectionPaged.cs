using AutoMapper;
using System.Collections.Generic;
using Yoshi.Infrastructure.Rest.PagedList;

namespace Yoshi.Infrastructure.Rest.Model
{
    public class RepresentationCollectionPaged<T> : RepresentationCollectionPaged<T, T> { }

    public class RepresentationCollectionPaged<TFrom, TTo>
    {
        #region Properties -------------------        
        public IEnumerable<TTo> Items { get; set; }
        public int TotalCount { get; set; }
        public LinkCollection Links { get; set; }
        #endregion
        #region Constructor ------------------
        public RepresentationCollectionPaged()
        {
            this.Links = new LinkCollection();
        }
        public RepresentationCollectionPaged(IPagedList<TFrom> items)
           : this()
        {
            this.TotalCount = items.TotalItemCount;
            this.Items = Mapper.Map<IEnumerable<TTo>>(items);
        }
        #endregion
    }
}
