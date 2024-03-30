<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Group extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name"
    ];
    
    public function users() : BelongsToMany
    {
        return $this->belongsToMany(User::class, "member","group","user_id");
    }
}
