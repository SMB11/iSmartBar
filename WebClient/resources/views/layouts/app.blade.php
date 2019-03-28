<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="{{asset('css/app.css')}}"/>
    <link href="{{ asset('css/bootstrap.min.css') }}" rel="stylesheet">
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
        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
        <!-- Include all compiled plugins (below), or include individual files as needed -->
        <script src="{{ asset('js/bootstrap.min.js') }}"></script>
</body>
</html>