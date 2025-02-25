using AutoMapper;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Mapper;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<InvoiceDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DateIssued, opt => opt.MapFrom(src => src.DateIssued))
            .ForMember(dest => dest.NetAmount, opt => opt.MapFrom(src => src.NetAmount))
            .ForMember(dest => dest.VatAmount, opt => opt.MapFrom(src => src.VatAmount))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.IssuerCompanyId, opt => opt.MapFrom(src => src.IssuerCompanyId))
            .ForMember(dest => dest.IssuerCompany, opt => opt.MapFrom(src => src.IssuerCompany))
            .ForMember(dest => dest.CounterPartyCompanyId, opt => opt.MapFrom(src => src.CounterPartyCompanyId))
            .ForMember(dest => dest.CounterPartyCompany, opt => opt.MapFrom(src => src.CounterPartyCompany))
            .AfterMap((src, dest) => dest = Invoice.Create(src.DateIssued, src.IssuerCompanyId, src.CounterPartyCompanyId, src.NetAmount, src.VatAmount, src.TotalAmount, src.Description));
    }
}
