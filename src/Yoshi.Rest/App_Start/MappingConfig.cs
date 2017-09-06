using Yoshi.QueryLayer.Mapping;
using Yoshi.QueryLayer.Model.Administration;
using Yoshi.Rest.Model.Administration;

namespace Yoshi.Rest
{
    public static class MappingConfig
    {
        public static void Register()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MerchantDto, MerchantRep>();
                cfg.CreateMap<MerchantDto, MerchantListRep>();
                cfg.AddProfile<QueryMappingProfile>();
            });
        }
    }
}