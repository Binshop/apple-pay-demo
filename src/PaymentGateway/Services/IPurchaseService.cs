// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.PaymentGateway.Messages;
using System.Threading.Tasks;

namespace Binshop.PaymentGateway.Services
{
    public interface IPurchaseService<TRequest, TResponse>
               where TRequest : RequestBase
               where TResponse : ResponseBase
    {
        Task<TResponse> PurchaseAsync(TRequest request);
    }
}
