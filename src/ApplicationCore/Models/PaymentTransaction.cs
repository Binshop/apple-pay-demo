// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.Models.Abstractions;

namespace Binshop.Models
{
    public class PaymentTransaction : BaseEntity
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// The access code used to query the payment result from eway platform.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The transaction ID on the payment gateway platform
        /// </summary>

        public string ExternalTransactionId { get; set; }
    }
}
