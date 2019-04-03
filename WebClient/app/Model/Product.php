<?php
namespace App\Model;

class Product
{
    public $ID;
    public $CategoryID;
    public $BrandID;
    public $Name;
    public $Description;
    public function __construct($ID, $CategoryID,$BrandID,$Name,$Description){
       $this->ID=$ID;
       $this->CategoryID=$CategoryID;
       $this->BrandID=$BrandID;
       $this->Name=$Name;
       $this->Description=$Description;
    }
}