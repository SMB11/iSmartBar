using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Constants
{
    public static class CommandNames
    {
        public const string OpenProductManager = "ProductsModule.OpenProductManager";
        public const string AddProduct = "ProductManager.Add";
        public const string EditProduct = "ProductManager.Edit";
        public const string SaveProduct = "ProductManager.Save";
        public const string RemoveProduct = "ProductManager.Remove";
        public const string CancelEditingProduct = "ProductManager.Cancel";


        public const string OpenBrandManager = "ProductsModule.OpenBrandManager";
        public const string AddBrand = "BrandManager.Add";
        public const string EditBrand = "BrandManager.Edit";
        public const string SaveBrand = "BrandManager.Save";
        public const string RemoveBrand = "BrandManager.Remove";
        public const string CancelEditingBrand = "BrandManager.Cancel";

    }
}
