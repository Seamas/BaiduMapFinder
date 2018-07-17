using System;
using AutoMapper;

namespace OSXBaiduMapSearch.Middle
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BaiduMap.Response.Models.PlaceDetailItem, PlaceDetail>()
                .ForMember(item => item.Lat, opt => opt.MapFrom(src => src.Location.Lat))
                .ForMember(item => item.Lng, opt => opt.MapFrom(src => src.Location.Lng));
        }
    }
}
