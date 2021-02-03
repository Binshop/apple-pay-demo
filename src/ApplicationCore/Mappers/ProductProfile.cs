// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using AutoMapper;
using Binshop.DTOs;
using Binshop.Models;

namespace Binshop.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
