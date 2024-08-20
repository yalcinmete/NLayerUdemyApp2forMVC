using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        //Task<List<ProductWithCategoryDto>> GetProductsWithCategory();

        //API controllerdeki actionlar da hep customResponseDto çevirme işlemi yapıyorduk.Bu işlemi controllerApı'da değil serviste yapmış olalım.
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
    }
}
