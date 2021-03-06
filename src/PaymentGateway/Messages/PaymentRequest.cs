// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

namespace Binshop.PaymentGateway.Messages
{
    /// <summary>
    ///  RAPID v5 Request
    /// </summary>
    public class PaymentRequest : RequestBase
    {
        public Payment Payment { get; set; }

        public PaymentInstrument PaymentInstrument { get; set; }

        public PaymentRequest()
        {
            TransactionType = "Ecom";
        }

        public PaymentRequest(string transactionType) : base(transactionType)
        {
        }
    }

    public class PaymentInstrument
    {
        public PaymentType PaymentType { get; set; }

        public WalletDetails WalletDetails { get; set; }
    }

    public class WalletDetails
    {
        /// <summary>
        /// JSON string for Apple Pay Payment Token Data, see https://developer.apple.com/library/archive/documentation/PassKit/Reference/PaymentTokenJSON/PaymentTokenJSON.html
        /// </summary>
        public string Token { get; set; }
    }
}
