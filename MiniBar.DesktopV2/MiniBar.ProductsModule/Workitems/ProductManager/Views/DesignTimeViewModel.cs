using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Interface;
using MiniBar.ProductsModule.Services;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    class DesignTimeViewModel : ProductManagerViewModel
    {
        public DesignTimeViewModel() : base(null, null, null, null, null)
        {
            throw new Exception("test exception");
            ListItems = new System.Collections.ObjectModel.ObservableCollection<EntityViewModels.Products.ProductViewModel>() {
                new EntityViewModels.Products.ProductViewModel{ ID = 1, Name = "test 1", Brand = "Brand 1", BrandID = 1, Category = "Category 1", CategoryID = 1,
                 Description = "asdasd", Price = 12.99f}
            };
        }
    }
}
