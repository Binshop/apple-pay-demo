// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.PaymentGateway.Constants;
using System.Net.Http;
using System.Threading.Tasks;

namespace Binshop.PaymentGateway.Services
{
    public class DefaultQueryService : IQueryService
    {
        private readonly GatewayAuthenticationBuilder _authBuilder;
        private readonly IUriComposer _uriComposer;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultQueryService(GatewayAuthenticationBuilder authBuilder,
                                   IUriComposer uriComposer,
                                   IHttpClientFactory httpClientFactory)
        {
            _authBuilder = authBuilder;
            _uriComposer = uriComposer;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetByTransactionId(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = _authBuilder.Build();

            var requestUri = _uriComposer.Build(ResourceTemplates.TEMPLATE_TRANSACTION_BY_ID, (ResourceTemplates.PARAM_NAME_TRANSACTION_ID, id));
            var response = await httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
