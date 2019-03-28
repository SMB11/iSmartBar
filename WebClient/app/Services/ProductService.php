<?php
namespace App\Services;
use App\Model\Product;
class ProductService{
    private static $products;

    private static function ensure(){
        if(ProductService::$products === null){
            ProductService::$products = array(
                //id,categoryID,$brandID,$name,$desc
                new Product(1, 3,3, "Product1", "Lorem ipusm"),
                new Product(2,3,3, "Product 2", "Lorem ipusm"),
                new Product(3,3,3, "Product 3", "Lorem ipusm"),
                new Product(4,3,3, "Product 4", "Lorem ipusm"),
                new Product(5,3,6, "Product 5", "Lorem ipusm"),
                new Product(6,3,6, "Product 6", "Lorem ipusm"),
                new Product(7,3,6, "Product 7", "Lorem ipusm"),
                new Product(8,4,4, "Product 8", "Lorem ipusm"),
                new Product(9,4,4, "Product 9", "Lorem ipusm"),
                new Product(10,3,3, "Product 10", "Lorem ipusm"),
            );
        }
    }

    public static function GetProduct($language,$categoryID,$brandID) //Check if the product is from our category
    {
        ProductService::ensure();
        $res = array();
        foreach(collect(ProductService::$products) as $pro){
            if($pro->CategoryID == $categoryID&&$pro->BrandID==$brandID){
                array_push($res,$pro);
            }
        }
        return collect($res);
    }

}



// Get From Api