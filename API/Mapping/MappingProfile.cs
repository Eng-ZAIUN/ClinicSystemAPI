using AutoMapper;
using BLL.DTOs;
using BLL.Model;
using DAL.Data;
using DAL.Model;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();

            CreateMap<Appointment, AppointmentDto>().ReverseMap();

            CreateMap<ApplicationUser, RegisterModel>().ReverseMap();
        }
    }
}
