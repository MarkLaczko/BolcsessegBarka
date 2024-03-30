<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\MorphTo;

class Note extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "text"
    ];

    public function courseable() : MorphTo
    {
        return $this->morphTo();
    }
}
