// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using System.Threading.Tasks;

namespace Binshop.PaymentGateway.Services
{
    public interface IQueryService
    {
        Task<string> GetByTransactionId(string id);
    }
}
