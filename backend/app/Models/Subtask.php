<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Subtask extends Model
{
    use HasFactory;

    public $timestamps = false;

    protected $fillable = [
        'task_id',
        'order',
        'question',
        'options',
        'solution',
        'type',
        'marks',
    ];

    public function task() : BelongsTo
    {
        return $this->belongsTo(Task::class);
    }
}
