<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Services\CategoryService;

class CategoriesController extends Controller
{
    public function index($language)
    {
        return view('global.index');
    }

    public function subcategories($language, $id)
    {
        $data = CategoryService::GetSubcategories($id, $language);
        return view('categories.root')->with('data',$data);
    }
}
