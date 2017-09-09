using Yoshi.EntityLayer.Model.Administration;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Mapping
{
    public class QueryMappingProfile : AutoMapper.Profile
    {
        public QueryMappingProfile()
        {
            CreateMap<Merchant, MerchantDto>();
<<<<<<< HEAD
            CreateMap<User, UserDto>();
=======
            CreateMap<Location, LocationDto>();
>>>>>>> c0d5705c3de6a62afa96813187e6f565a418824a
        }
    }
}
