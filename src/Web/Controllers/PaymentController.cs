// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.DTOs.ApplePay;
using Binshop.DTOs.Payments;
using Binshop.Models;
using Binshop.PaymentGateway;
using Binshop.PaymentGateway.Messages;
using Binshop.PaymentGateway.Services;
using Binshop.Repositories;
using Binshop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Binshop.Controllers.Payments
{
    [Route(Constants.ROUTE_TEMPLATE_CONTROLLER_ACTION)]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplePayMerchantValidator _merchantValidator;
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<PaymentTransaction> _transactionRepository;
        private readonly IPurchaseService<PaymentRequest, PaymentResponse> _purchaseService;
        private readonly JsonSerializerOptions _serializerOptions;

        public PaymentController(ApplePayMerchantValidator applePayMerchantValidator,
                                 IAsyncRepository<Product> productRepository,
                                 IAsyncRepository<Order> orderRepository,
                                 IAsyncRepository<PaymentTransaction> transactionRepository,
                                 IPurchaseService<PaymentRequest, PaymentResponse> purchaseService)
        {
            _merchantValidator = applePayMerchantValidator;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _transactionRepository = transactionRepository;
            _purchaseService = purchaseService;

            _serializerOptions = new JsonSerializerOptions
            {
                // PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
            _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAsync([FromBody] MerchantValidationRequest validation)
        {
            // You may wish to additionally validate that the URI specified for merchant validation in the
            // request body is a documented Apple Pay JS hostname. The IP addresses and DNS hostnames of
            // these servers are available here: https://developer.apple.com/documentation/applepayjs/setting_up_server_requirements
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var initiativeContext = Request.GetTypedHeaders().Host.Value; // the domain of your server which handling the payments.
            var sessionResponse = await _merchantValidator.ValidateAsync(validation.ValidationUrl, initiativeContext);

            var responsePayload = new MerchantValidationResponse
            {
                MerchantSession = sessionResponse
            };

            return Ok(responsePayload);
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseAsync([FromBody] PurchaseRequest purchaseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await _productRepository.GetByIdAsync(purchaseRequest.ProductId);

            if (product == null)
            {
                return BadRequest();
            }

            var newOrder = await _orderRepository.AddAsync(new Order
            {
                Price = product.Price,
                ProductId = product.Id,
                Unit = 1,
            });

            var accessCodeRequest = new PaymentRequest
            {
                Payment = new Payment
                {
                    CurrencyCode = "AUD",
                    TotalAmount = (int)(newOrder.Price * 100) * newOrder.Unit
                },
                PaymentInstrument = new PaymentInstrument
                {
                    PaymentType = PaymentType.ApplePay,
                    WalletDetails = new WalletDetails
                    {
                        Token = JsonSerializer.Serialize(purchaseRequest.ApplePayToken.PaymentData, _serializerOptions)
                    }
                }
            };

            var newTransaction = await _transactionRepository.AddAsync(new PaymentTransaction
            {
                OrderId = newOrder.Id
            });

            var purchaseResponse = await _purchaseService.PurchaseAsync(accessCodeRequest);

            if (!string.IsNullOrEmpty(purchaseResponse.Errors)
              || string.IsNullOrEmpty(purchaseResponse.TransactionId))
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }

            newTransaction.ExternalTransactionId = purchaseResponse.TransactionId;

            return Ok(new PurchaseResponse { OrderId = newOrder.Id });
        }
    }
}
