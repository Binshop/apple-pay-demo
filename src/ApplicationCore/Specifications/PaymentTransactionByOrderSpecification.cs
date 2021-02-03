// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Ardalis.Specification;
using Binshop.Models;
using System.Linq;

namespace Binshop.Specifications
{
    public class PaymentTransactionByOrderSpecification : Specification<PaymentTransaction>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">Order id list</param>
        public PaymentTransactionByOrderSpecification(params int[] ids)
        {
            Query.Where(t => ids.Contains(t.OrderId));
        }
    }
}
