<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="{{asset('css/app.css')}}"/>
        <title>{{config('app.name','laravelFirst')}}</title>
</head>
<body>
    <ul>
        @foreach ($data as $category)
            <li>{{$category->name}}</li>
        @endforeach
    </ul>
</body>
</html>