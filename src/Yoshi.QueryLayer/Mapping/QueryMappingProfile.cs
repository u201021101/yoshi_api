using Yoshi.EntityLayer.Model.Administration;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Mapping
{
    public class QueryMappingProfile : AutoMapper.Profile
    {
        public QueryMappingProfile()
        {
            CreateMap<Merchant, MerchantDto>();
            CreateMap<User, UserDto>();
        }
    }
}
