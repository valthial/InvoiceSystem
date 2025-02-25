using AutoMapper;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.IssuerCompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .AfterMap((src, dest) => dest = User.Create(src.Email, src.Password, src.CompanyId));
    }
}
