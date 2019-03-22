<?php
namespace App\Services;
use App\Model\Category;
use Illuminate\Support\Str;
class CategoryService{

    private static $categories;

    private static function ensure(){
        if(CategoryService::$categories === null){
            CategoryService::$categories = array(
                new Category(1, "Food"),
                new Category(2, "Drinks"),
                new Category(3, "Vegeterian", 1),
                new Category(4, "Snacks", 1),
                new Category(5, "Alcoholic", 2),
                new Category(6, "Non-Alcoholic", 2),
            );
        }
    }

    public static function GetRoot($language){
        CategoryService::ensure();
        return collect(array_filter(CategoryService::$categories, 
                function($cat){ 
                    return $cat->parentID === null; 
                }
            ));
    }
    
    public static function GetSubcategories($id, $language){
        
        CategoryService::ensure();
        return collect(array_filter(CategoryService::$categories, 
                function($cat) use( &$id){ 
                    return $cat->parentID == $id; 
                }
            ));
    }

    public static function GetTree($language){
        CategoryService::ensure();
        return collect(CategoryService::$categories);
    }
}



// Get From Api