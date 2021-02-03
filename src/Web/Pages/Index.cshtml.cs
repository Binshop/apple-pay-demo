// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using AutoMapper;
using Binshop.DTOs;
using Binshop.Models;
using Binshop.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public IReadOnlyList<ProductDto> Products { get; set; }

        public IndexModel(IAsyncRepository<Product> productRepository,
                          IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task OnGetAsync()
        {
            var products = await _productRepository.ListAllAsync();
            Products = _mapper.Map<List<ProductDto>>(products);
        }
    }
}
