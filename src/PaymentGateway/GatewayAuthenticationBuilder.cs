// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.PaymentGateway.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Binshop.PaymentGateway
{
    public class GatewayAuthenticationBuilder
    {
        private readonly IOptionsMonitor<GatewayCredential> _optionsMonitor;

        public GatewayAuthenticationBuilder(IOptionsMonitor<GatewayCredential> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public AuthenticationHeaderValue Build()
        {
            var account = $"{_optionsMonitor.CurrentValue.Key}:{_optionsMonitor.CurrentValue.Secret}";
            var bytes = Encoding.ASCII.GetBytes(account);
            var base64 = Convert.ToBase64String(bytes);
            return new AuthenticationHeaderValue("Basic", base64);
        }
    }
}
