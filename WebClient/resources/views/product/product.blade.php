<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="{{asset('css/app.css')}}"/>
    <title>{{config('app.name','laravelFirst')}}</title>
</head>
<body>
                <div class="container">
                <ul class="list-group">
                        <div class="row">
                        @foreach ($data as $product)
                            <li class="list-group-item"><div class="col">
                                <h2>{{$product->name}}</h2>
                                <p>{{$product->description}}</p>
                            </div>
                            </li>
                        @endforeach
                    </div>
                    </ul>
                </div>
               
            
</body>
</html>