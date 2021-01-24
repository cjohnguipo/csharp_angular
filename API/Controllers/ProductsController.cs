using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _repoProducts;
        private readonly IGenericRepository<ProductBrand> _repoProductBrands;
        private readonly IGenericRepository<ProductType> _repoProductTypes;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> repoProducts,
            IGenericRepository<ProductBrand> repoProductBrands,
            IGenericRepository<ProductType> repoProductTypes,
            IMapper mapper)
        {
            _repoProducts = repoProducts;
            _repoProductBrands = repoProductBrands;
            _repoProductTypes = repoProductTypes;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _repoProducts.ListAsync(spec);
            // return Ok(products);
            // return products.Select(product => new ProductToReturnDTO 
            //  {  
            //    Id = product.Id,     
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    ProductType  = product.ProductType.Name,
            //    ProductBrand  = product.ProductBrand.Name
            //  }).ToList();

            return Ok( _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _repoProducts.GetEntityWithSpec(spec);

            // return new ProductToReturnDTO
            // {
            //    Id = product.Id,     
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    ProductType  = product.ProductType.Name,
            //    ProductBrand  = product.ProductBrand.Name
            // };

            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDTO>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            var productBrands = await _repoProductBrands.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            var productTypes = await _repoProductTypes.ListAllAsync();
            return Ok(productTypes);
        }

    }
}
