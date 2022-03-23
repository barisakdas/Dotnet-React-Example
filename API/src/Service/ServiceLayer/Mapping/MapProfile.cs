namespace ServiceLayer.Mapping;
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<HotelEntity, HotelDto>().ReverseMap();
        CreateMap<HotelEntity, HotelCreateDto>().ReverseMap();
        CreateMap<HotelEntity, HotelUpdateDto>().ReverseMap();
    }
}

