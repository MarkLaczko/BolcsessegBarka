<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Note extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "title",
        "text",
        "topic_id",
        "user_id",
        "role"
    ];

    public function topic(): BelongsTo
    {
        return $this->belongsTo(Topic::class);  
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class);  
    }
}
