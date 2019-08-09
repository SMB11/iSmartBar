using AutoMapper;
using MiniBar.EntityViewModels.Products;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.EntityViewModels.Products.Profiles
{
    public class BrandProfile : Profile
    {

        public BrandProfile()
        {
            this.CreateMap<BrandDTO, BrandViewModel>();
        }
    }
}
