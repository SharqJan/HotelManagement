using AutoMapper;
using SMSC.Application.DTO;
using SMSC.Core.Entities;

namespace SMSC.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // employee
            CreateMap<Employee, EmployeeDto>();

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<Roles, RoleDTO>();

            CreateMap<Roles, RoleDTO>().ReverseMap();

            CreateMap<Users,UserDTO>();

            CreateMap<Users, UserDTO>().ReverseMap();

            CreateMap<Service, ServiceDTO>();

            CreateMap<Service, ServiceDTO>().ReverseMap();

            CreateMap<Room, RoomDTO>();

            CreateMap<Room, RoomDTO>().ReverseMap();

            CreateMap<Guest, GuestDTO>();

            CreateMap<Guest, GuestDTO>().ReverseMap();

            CreateMap<Reservation, ReservationDto>();

            CreateMap<Reservation, ReservationDto>().ReverseMap();





        }
    }
}
