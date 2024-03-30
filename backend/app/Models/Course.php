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

    public function notes() : MorphMany
    {
        return $this->morphMany(Note::class, "courseable");
    }

    public function quizzes() : MorphMany
    {
        return $this->morphMany(Quiz::class, "courseable");
    }

    public function assignments() : MorphMany
    {
        return $this->morphMany(Assignment::class, "courseable");
    }
}
