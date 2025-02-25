using AutoMapper;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Mapper;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .AfterMap((src, dest) => dest = Company.Create(src.Name));
    }
}
