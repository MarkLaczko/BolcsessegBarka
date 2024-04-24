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
        "topic_id"
    ];

    public function topic() : BelongsTo
    {
        return $this->belongsTo(Topic::class);
    }
}
