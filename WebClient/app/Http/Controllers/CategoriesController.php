<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Services\CategoryService;

class CategoriesController extends Controller
{
    public function index()
    {
        $data = CategoryService::GetRoot("en");
        return view('categories.root')->with('data',$data);
    }
}
