using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Yoshi.Infrastructure.Rest.OData
{
    public class ODataQueryModelBinder : IModelBinder
    {
        #region IModelBinder
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(ODataQuery))
            {
                return false;
            }

            var query = actionContext.Request.RequestUri.Query;
            if (string.IsNullOrWhiteSpace(query))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "wrong value");
            }

            bindingContext.Model = ODataExpressions.Parse(query);

            return true;
        }
        #endregion
    }
}
