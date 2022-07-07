using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Insurances.DTO
{
    public class InsuranceMappingProfile : Profile
    {
        public InsuranceMappingProfile()
        {
            CreateMap<Insurance, InsuranceDTO>();
            CreateMap<InsuranceDTO, Insurance>();
        }
    }
}
