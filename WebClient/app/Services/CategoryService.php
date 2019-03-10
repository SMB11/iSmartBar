<?php
namespace App\Services;
use App\Model\Category;
class CategoryService{

    public static function GetRoot($language){
        $cat = new Category();
        $cat->id = 1;
        $cat->name = "Food";
        
        $cat2 = new Category();
        $cat2->id = 2;
        $cat2->name = "Drinks";
        return array($cat, $cat2);
    }
}



// Get From Api