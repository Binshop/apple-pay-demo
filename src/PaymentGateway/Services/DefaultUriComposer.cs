// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.PaymentGateway.Constants;
using Binshop.PaymentGateway.Options;
using Microsoft.Extensions.Options;

namespace Binshop.PaymentGateway.Services
{
    public class DefaultUriComposer : IUriComposer
    {
        private readonly IOptionsMonitor<GatewayOptions> _optionsMonitor;

        public DefaultUriComposer(IOptionsMonitor<GatewayOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public string Build(string template, params (string name, string value)[] @params)
        {
            var endpoint = _optionsMonitor.CurrentValue.Endpoint;

            var resourceUri = template.Replace(ResourceTemplates.PARAM_NAME_ENDPOINT, endpoint);

            foreach (var (name, value) in @params)
            {
                resourceUri = resourceUri.Replace(name, value);
            }

            return resourceUri;
        }
    }
}
