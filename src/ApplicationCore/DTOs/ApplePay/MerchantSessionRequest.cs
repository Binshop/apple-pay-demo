// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

namespace Binshop.DTOs.ApplePay
{
    public class MerchantSessionRequest
    {
        public string MerchantIdentifier { get; set; }

        public string DisplayName { get; set; }

        public string Initiative { get; set; }

        public string InitiativeContext { get; set; }
    }
}
