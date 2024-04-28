<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Casts\Attribute;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\MorphTo;

class Quiz extends Model
{
    protected $table = "quizzes";
    public $timestamps = false;

    protected $fillable = [
        'name',
        'max_attempts',
        'opens',
        'closes',
        'time',
        'topic_id',
    ];

    public function topic() : BelongsTo
    {
        return $this->belongsTo(Topic::class);
    }

    public function tasks() : HasMany
    {
        return $this->hasMany(Task::class);
    }

    public function tasks_count() {
        return $this->tasks()->count();
    }

    public function attempts() : HasMany
    {
        return $this->hasMany(Attempt::class);
    }
}

