using Ambev.DeveloperEvaluation.Application.Dtos;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Mappings.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequest, Sale>();
            CreateMap<SaleItemRequest, SaleItem>();

            CreateMap<SaleItem, SaleItemResponse>();

            CreateMap<Sale, SaleResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items ?? new List<SaleItem>()));

        }
    }
}
