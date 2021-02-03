// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using AutoMapper;
using Binshop.DTOs;
using Binshop.Models;

namespace Binshop.Mappers
{
    public class PaymentTransactionProfile : Profile
    {
        public PaymentTransactionProfile()
        {
            CreateMap<PaymentTransaction, PaymentTransactionDto>();

            CreateMap<PaymentTransactionDto, PaymentTransaction>();
        }
    }
}
