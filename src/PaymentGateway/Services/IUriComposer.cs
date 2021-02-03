// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

namespace Binshop.PaymentGateway.Services
{
    public interface IUriComposer
    {
        string Build(string template, params (string name, string value)[] @params);
    }
}
