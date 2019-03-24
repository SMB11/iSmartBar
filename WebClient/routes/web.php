<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::group(['middleware' => 'localization'], function(){
    
    Route::get('/', 'LanguagesController@index');
    Route::get('/{language}', 'CategoriesController@index');
    Route::get('/{language}/product', 'ProductController@index');

    Route::get('/{language}/category/{id}', 'CategoriesController@subcategories')->name('category');
    Route::get('/{language}/brand/{id}', 'ProductController@index')->name("brand");
    // Route::get('/{language}/about', 'PagesController@about');
    // Route::get('/{language}/services', 'PagesController@services');
    // Route::resource('/{language}/posts','PostsController');
    

});
