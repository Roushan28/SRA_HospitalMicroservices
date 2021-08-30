using AutoMapper;
using PharmacyDetails.API.Dtos;
using PharmacyDetails.API.Entities;

namespace PharmacyDetails
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PharmacyDetail, GetDetailDto>();
            CreateMap<AddDetailDto, PharmacyDetail>();

        }
    }
}