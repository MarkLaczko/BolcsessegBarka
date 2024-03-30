<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\MorphTo;

class Quiz extends Model
{
    protected $table = "quizzes";
    public $timestamps = false;

    protected $fillable = [
        "attempts",
        "name",
        "opens",
        "closes",
        "time",
        "grade"
    ];

    public function courseable() : MorphTo
    {
        return $this->morphTo();
    }
}

