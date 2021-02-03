// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Ardalis.Specification;
using Binshop.Models;
using System.Linq;

namespace Binshop.Specifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification(params int[] ids)
        {
            Query.Where(c => ids.Contains(c.Id));
        }
    }
}
