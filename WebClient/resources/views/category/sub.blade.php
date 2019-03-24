@extends('layouts.app')

@section('content')
    <ul class="category-sub">
        @foreach ($subCategories as $category)
            <li>
                <a >{{$category->name}}</a>
                <ul>
                    
                    @foreach ($brands[strval($category->id)] as $brand)
                        <li><a href={{ route('brand', [$language, $brand->id])}}>{{$brand->name}}</a></li>
                    @endforeach
                </ul>
            </li>
        @endforeach
    </ul>
@endsection