<?php
namespace App\Services;
use App\Model\Product;
class ProductService{

    public static function makeProduct($language)    //Make Products(Get from API)
    {
        $prod = new Product();
        $prod->id=1;
        $prod->CategoryID=3;
        $prod->BrandID=22;
        $prod->name="Cola";
        $prod->description="Lorem Ipsum Some text";

        $prod2 = new Product();
        $prod2->id=2;
        $prod2->CategoryID=4;
        $prod2->BrandID=23;
        $prod2->name="Fanta";
        $prod2->description="Lorem Ipsum Some text";
        
        $arr  = array($prod, $prod2);

        return $arr;
    }

    public static function showProduct($categoryID,$arr,$products = array()) //Check if the product is from our category
    {

        foreach($arr as $pro){
            if($pro->CategoryID == $categoryID){
                array_push($products,$pro);
            }
        }
        return $products;
    }

}



// Get From Api