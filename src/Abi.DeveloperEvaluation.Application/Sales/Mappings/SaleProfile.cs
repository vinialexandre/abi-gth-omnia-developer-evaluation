using Abi.DeveloperEvaluation.Application.Dtos;
using AutoMapper;
using Abi.DeveloperEvaluation.Domain.Entities;

namespace Abi.DeveloperEvaluation.Mappings.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequest, Sale>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<SaleItemRequest, SaleItem>();

            CreateMap<SaleItem, SaleItemResponse>();

            CreateMap<Sale, SaleResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items ?? new List<SaleItem>()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
