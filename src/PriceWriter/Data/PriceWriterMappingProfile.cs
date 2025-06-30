using AutoMapper;

namespace PriceWriter.Data;

public class PriceWriterMappingProfile : Profile
{
    public PriceWriterMappingProfile()
    {
        CreateMap<Price, PriceData>();
        CreateMap<PriceData, Price>();
    }
}