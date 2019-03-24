<?php
namespace App\Model;

class Brand
{

    public function __construct($id, $name){
        $this->id = $id;
        $this->name = $name;
    }

    public $id;
    public $name;
}