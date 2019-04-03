@extends('layouts.app')

@section('content')
    
        <h2>Products for the brand with id {{$brandID}}</h2>  
        <ul class="brand products row"> 
        @foreach ($products as $product)
    <li><div class="card col" style="padding:10px; margin-right:30px">
        <a class="card-title" href="#">{{$product->Name}}</a>
        <p class="card-text">{{$product->Description}}</p>
    </div></li>
        @endforeach
    </ul>
@endsection