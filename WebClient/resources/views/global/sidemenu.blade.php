<ul>
    @foreach ($tree->filter(
            function($cat){ 
                return $cat->parentID === null; 
            }
        ) as $category)
            <li><a href={{ route('category', [$language, $category->id])}}>{{$category->name}}</a></li>
            <ul>
                @foreach ($tree->filter(
                    function($cat) use( &$category){ 
                        return $cat->parentID === $category->id; 
                    }
                ) as $child)
                    <li><a href={{ route('category', [$language, $category->id]).'#'.$child->name}}>{{$child->name}}</a></li>
                @endforeach

            </ul>
    @endforeach
</ul>