<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Attempt extends Model
{
    use HasFactory;

    protected $fillable = [
        'quiz_id',
        'user_id',
        'start',
        'end',
        'marks',
        'grade',
    ];

    public $timestamps = false;

    public function quiz() : BelongsTo 
    {
        return $this->belongsTo(Quiz::class);
    }

    public function user() : BelongsTo
    {
        return $this->belongsTo(User::class);
    }

    public function answers() : HasMany
    {
        return $this->hasMany(Answer::class);
    }
}
