using AutoMapper;
using Base_API_Structure.DTOs;
using Base_API_Structure.Models;

namespace Base_API_Structure.Mappings
{
    public class BaseMapper : Profile 
    {
        public BaseMapper() 
        {
            //Source -> Target
            CreateMap<BaseModel, BaseReadDTO>();
            CreateMap<BaseModel, BaseUpdateDTO>();
            CreateMap<BaseCreateDTO, BaseModel>();
            CreateMap<BaseUpdateDTO, BaseModel>();
        }
    }
}