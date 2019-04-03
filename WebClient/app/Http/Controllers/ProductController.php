<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Services\ProductService;

class ProductController extends Controller
{
    public function index($language,$id,$brandID)
    {
        //Hard coded but principle is same, Show products, from concrete category.
        $data = ProductService::GetProduct($language,$id,$brandID); 
        return view('product.product')->with('products',$data)->with('brandID',$brandID);
    }
}
