<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Services\CategoryService;
use App\Services\BrandService;

class CategoriesController extends Controller
{
    public function index($language)
    {
        return view('global.index');
    }

    public function subcategories($language, $id)
    {
        $subCategories = CategoryService::GetSubcategories($id, $language);
        $brands = BrandService::GetCategoryBrands($subCategories->map(function($cat) { return $cat->id; }));
        return view('category.sub')
            ->with('subCategories', $subCategories)
            ->with('brands', $brands);
    }
}
