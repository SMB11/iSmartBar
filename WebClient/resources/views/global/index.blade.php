@extends('layouts.app')

@section('content')
    <ul class="category-root">
        @foreach ($tree->filter(
            function($cat){ 
                return $cat->parentID === null; 
            }
        ) as $category)
            <li><a href={{ route('category', [$language, $category->id])}}>{{$category->name}}</a></li>
        @endforeach
    </ul>
@endsection