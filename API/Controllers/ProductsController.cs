using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using API.Errors;

namespace API.Controllers
{    
    public class ProductsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        public ProductsController(
            IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;            
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

    [HttpGet]    
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(string sort, int? brandId, int? typeId)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
        var products = await _productsRepo.ListAsync(spec);
        var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(mappedProducts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productsRepo.GetEntityWithSpec(spec);

        if(product == null) return NotFound(new ApiResponse(404));

        var mappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
        return Ok(mappedProduct);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }
}
}