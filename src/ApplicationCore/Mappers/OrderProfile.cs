// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using AutoMapper;
using Binshop.DTOs;
using Binshop.Models;

namespace Binshop.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
