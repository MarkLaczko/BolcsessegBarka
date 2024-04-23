<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Task extends Model
{
    public $timestamps = false;

    protected $fillable = [
        'quiz_id',
        'order',
        'header',
        'text',
    ];

    public function quiz() : BelongsTo
    {
        return $this->belongsTo(Quiz::class);
    }

    public function subtasks() : HasMany
    {
        return $this->hasMany(Subtask::class);
    }
}
