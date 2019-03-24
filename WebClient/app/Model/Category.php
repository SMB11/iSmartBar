<?php
namespace App\Model;

class Category
{

    public function __construct($id, $name, $parentID = null){
        $this->id = $id;
        $this->name = $name;
        $this->parentID = $parentID;
    }

    public $id;
    public $name;
    public $parentID;
}