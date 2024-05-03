<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\MorphMany;

class Topic extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name",
    ];

    public function notes() : HasMany
    {
        return $this->hasMany(Note::class);
    }

    public function quizzes() : HasMany
    {
        return $this->hasMany(Quiz::class);
    }

    public function assignments() : HasMany
    {
        return $this->hasMany(Assignment::class);
    }

    public function course() : BelongsTo
    {
        return $this->belongsTo(Course::class);
    }
}
