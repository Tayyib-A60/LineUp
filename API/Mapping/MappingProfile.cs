using API.Controllers.DTOs;
using API.Core.Models;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // CreateMap<Source, Destination>();
            
            // API to Domain
            CreateMap<AmenityDTO, Amenity>();
            CreateMap<BookingDTO, Booking>();
            CreateMap<ChatDTO, Chat>();
            CreateMap<EnquiryDTO, Enquiry>();
            CreateMap<SpaceDTO, Space>();
            CreateMap<SpaceToUpdateDTO, Space>();
            CreateMap<SpaceTypeDTO, SpaceTypeDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserToLoginDTO, User>();
            CreateMap<UserToSignUpDTO, User>();
            CreateMap<SpaceQueryDTO, SpaceQuery>();
            CreateMap<BookingQueryDTO, BookingQuery>();

            // Domain to API Resource
            CreateMap<Amenity, AmenityDTO>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<Chat, ChatDTO>();
            CreateMap<Enquiry, EnquiryDTO>();
            CreateMap<Space, SpaceDTO>();
            CreateMap<Space, SpaceToUpdateDTO>();
            CreateMap<SpaceType, SpaceType>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserToLoginDTO>();
            CreateMap<User, UserToSignUpDTO>();

        }
    }
}