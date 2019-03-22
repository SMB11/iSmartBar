<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="{{asset('css/app.css')}}"/>
        <title>{{config('app.name','laravelFirst')}}</title>
</head>
<body>
    <div class="main container">
        <div class="sidemneu">
            @include('global.sidemenu')
        </div>
        <div class="content">
            @yield('content')
        </div>
    </div>
</body>
</html>