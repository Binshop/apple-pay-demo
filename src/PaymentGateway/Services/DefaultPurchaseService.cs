// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.PaymentGateway.Constants;
using Binshop.PaymentGateway.Messages;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Binshop.PaymentGateway.Services
{
    public class DefaultPurchaseService : IPurchaseService<PaymentRequest, PaymentResponse>
    {
        private readonly GatewayAuthenticationBuilder _authBuilder;
        private readonly IUriComposer _uriComposer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;

        public DefaultPurchaseService(GatewayAuthenticationBuilder authBuilder,
                                    IUriComposer uriComposer,
                                    IHttpClientFactory httpClientFactory)
        {
            _authBuilder = authBuilder;
            _uriComposer = uriComposer;
            _httpClientFactory = httpClientFactory;

            _serializerOptions = new JsonSerializerOptions
            {
                // PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
            _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<PaymentResponse> PurchaseAsync(PaymentRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = _authBuilder.Build();
            var jsonBody = JsonSerializer.Serialize(request, _serializerOptions);
            var stringContent = new StringContent(jsonBody, Encoding.UTF8, MediaTypeNames.Application.Json);

            var requestUri = _uriComposer.Build(ResourceTemplates.TEMPLATE_TRANSACTION_V5);

            var response = await httpClient.PostAsync(requestUri, stringContent);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PaymentResponse>(json);
        }
    }
}
