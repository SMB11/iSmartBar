<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Services\CategoryService;

class CategoriesController extends Controller
{
    public function index($language)
    {
        $data = CategoryService::GetRoot($language);
        return view('categories.root')->with('data',$data);
    }

    public function subcategories($language, $id)
    {
        $data = CategoryService::GetSubcategories($id, $language);
        return view('categories.root')->with('data',$data);
    }
}
