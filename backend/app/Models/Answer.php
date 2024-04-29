<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Answer extends Model
{
    use HasFactory;

    protected $fillable = [
        'attempt_id',
        'subtask_id',
        'answer',
        'correct',
        'marks'
    ];

    public $timestamps = false;

    public function attempt() : BelongsTo 
    {
        return $this->belongsTo(Attempt::class);
    }

    public function subtask() : BelongsTo
    {
        return $this->belongsTo(Subtask::class);
    }
}
