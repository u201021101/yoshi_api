using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoshi.QueryLayer.Model.Administration
{
    public class MerchantDto
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string TaxId { get; set; }
        public bool Active { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
