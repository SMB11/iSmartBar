using AutoMapper;
using MiniBar.EntityViewModels.Products;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Configuration
{
    class CategoryProfile : Profile
    {

        public CategoryProfile()
        {
            this.CreateMap<CategoryDTO, CategoryViewModel>();
        }
    }
}
