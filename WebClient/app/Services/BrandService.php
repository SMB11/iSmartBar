<?php
namespace App\Services;
use App\Model\Brand;
use Illuminate\Support\Str;
class BrandService{

    private static $brands;

    private static function ensure(){
        if(BrandService::$brands === null){
            BrandService::$brands = array(
                new Brand(1, "Snickers"),
                new Brand(2, "Mars"),
                new Brand(3, "Twix"),
                new Brand(4, "Test"),
                new Brand(5, "Heineken"),
                new Brand(6, "Kilikia"),
                new Brand(7, "Grants"),
                new Brand(8, "Milka"),
                new Brand(9, "Stars"),
                new Brand(10, "Test 2"),
            );
        }
    }

    public static function GetCategoryBrands($categoryIDs){
        BrandService::ensure();
        $res = array();
        foreach($categoryIDs as $categoryID)
            $res[strval($categoryID)] = collect(BrandService::$brands)->filter(function($brand) use(&$categoryID){ return $brand->id%$categoryID == 0; });
        return collect($res);
    }
}



// Get From Api