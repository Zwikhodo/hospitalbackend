using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Medicines.DTO
{
    public class MedicineMappingProfile : Profile
    {
        public MedicineMappingProfile()
        {
            CreateMap<Medicine, MedicineDTO>();
            CreateMap<MedicineDTO, Medicine>();
        }
    }
}
