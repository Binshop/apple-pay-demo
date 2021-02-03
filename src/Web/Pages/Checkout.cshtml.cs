// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using AutoMapper;
using Binshop.DTOs;
using Binshop.Models;
using Binshop.Options;
using Binshop.Repositories;
using Binshop.Services;
using Binshop.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.Tasks;

namespace Binshop.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductDto Product { get; set; }

        public RegionInfo Region { get; set; }

        public string CurrencyCode => Region.ISOCurrencySymbol;

        public string CountryCode => Region.TwoLetterISORegionName;

        public string ValidationUrl { get; set; }

        public string PurchaseUrl { get; set; }

        public string MerchantIdentifier { get; set; }

        public bool EchoMode { get; set; }

        public CheckoutModel(IAsyncRepository<Product> productRepository,
                             IMapper mapper,
                             ApplePayCertificateProvider applePayCertificateProvider,
                             IOptionsMonitor<ApplePayOptions> applePayOptions)
        {
            _productRepository = productRepository;
            _mapper = mapper;

            MerchantIdentifier = applePayCertificateProvider.GetMerchantIdentifier();
            EchoMode = applePayOptions.CurrentValue.EchoMode;

            Region = new RegionInfo("en-AU");
        }

        public async Task OnGetAsync(int id)
        {
            ValidationUrl = Url.Action("Validate", "Payment");
            PurchaseUrl = Url.Action("Purchase", "DirectPayment");

            var spec = new ProductSpecification(new[] { id });
            var product = await _productRepository.FirstOrDefaultAsync(spec);
            Product = _mapper.Map<ProductDto>(product);
        }
    }
}
