﻿using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceNoCaching : Service<Product> , IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        //public async Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            //Video33._productRepository.GetProductsWithCategory() List<Product> dönüüyor ama bize List<ProductWithCategoryDto> dönüş tipi lazım.Mapperla biz bunu rahatlıkla yapabiliriz ama bir tık ileriye taşıyalım olayı IProductService interfacesinde direkt Task<CustomResponseDto> dönelim zaten API controllerdeki actionlar da hep customResponseDto çevirme işlemi yapıyorduk. Bu işlemi controllerApı'da değil serviste yapmış olalım.
            var products = await _productRepository.GetProductsWithCategory();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

            //API'nın istemiş olduğu CustomResponseDto<List<ProductWithCategoryDto>> datayı dönmüş olduk.
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
