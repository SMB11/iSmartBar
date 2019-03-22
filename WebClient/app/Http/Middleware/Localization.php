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
        $newLangauge = $urlSegments[0];
        $currentLanguage = App::getLocale();
        
        view()->share('language', $newLangauge);
        view()->share('tree', App\Services\CategoryService::GetTree($newLangauge));
        
        if($currentLanguage !== $currentLanguage){
            App::setLocale($newLangauge);
            handleLanguageChanged($newLangauge);
        }

        return $next($request);
    }

    private function handleLanguageChanged($language){
        
    }
}
