<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Services\ProductService;

class ProductController extends Controller
{
    public function index($language,$id)
    {
        //Hard coded but principle is same, Show products, from concrete category.
        $data = ProductService::ShowProduct($id,ProductService::makeProduct($language)); 
        return view('product.product')->with('brandID',$id);
    }
}
