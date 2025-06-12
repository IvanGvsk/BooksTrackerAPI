using AutoMapper;
using BookTrackerApi.Models;

namespace BookTrackerApi.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterDto, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
            CreateMap<BookDto, Book>().ReverseMap();
        }
    }
}
