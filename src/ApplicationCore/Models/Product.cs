// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.Models.Abstractions;

namespace Binshop.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }
    }
}
