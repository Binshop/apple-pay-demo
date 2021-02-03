// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using System.ComponentModel.DataAnnotations;

namespace Binshop.DTOs.ApplePay
{
    public class MerchantValidationRequest
    {
        [Required]
        [DataType(DataType.Url)]
        public string ValidationUrl { get; set; }
    }
}
