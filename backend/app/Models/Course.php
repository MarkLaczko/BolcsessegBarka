<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\MorphMany;

class Course extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name",
        "image"
    ];
    
    public function groups() : BelongsToMany
    {
        return $this->belongsToMany(Group::class, "course_group");
    }
}
