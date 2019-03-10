<?php

namespace App\Http\Middleware;

use Closure;
use App;
class Localization
{
    /**
     * Handle an incoming request.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \Closure  $next
     * @return mixed
     */
    public function handle($request, Closure $next)
    {
        $urlSegments = explode('/', $request->path());
        view()->share('language', $urlSegments[0]);
        App::setLocale($urlSegments[0]); // Laravel locale is set to $language now

        return $next($request);
    }
}
