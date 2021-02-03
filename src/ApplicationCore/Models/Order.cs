// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Binshop.Models.Abstractions;

namespace Binshop.Models
{
    public class Order : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Unit { get; set; }

        public decimal Amount => Price * Unit;
    }
}
