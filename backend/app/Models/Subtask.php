<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Subtask extends Model
{
    use HasFactory;

    public $timestamps = false;

    protected $fillable = [
        'task_id',
        'order',
        'question',
        'options',
        'type',
        'marks',
    ];

    public function task() : BelongsTo
    {
        return $this->belongsTo(Task::class);
    }

    public function answers() : HasMany
    {
        return $this->hasMany(Answer::class);
    }
}
