using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogics.DTO;
using DataAcess.DataModels;

namespace BusinessLogics.Mapping
{
    /// <summary>
    /// This class handles the mapping between the DTOs and their models
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StandDTO, Stand>();
            CreateMap<Stand, StandDTO>();

            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();

            CreateMap<MallDTO, Mall>();
            CreateMap<Mall, MallDTO>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
