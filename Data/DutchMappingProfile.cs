using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Data
{
    public class DutchMappingProfile: Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(vw => vw.OrderId, o => o.MapFrom(o => o.Id))
                .ReverseMap();
        }
    }
}