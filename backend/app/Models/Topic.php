<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\MorphMany;

class Topic extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name",
        "order"
    ];

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

    public function course() : BelongsTo
    {
        return $this->belongsTo(Course::class);
    }
}
