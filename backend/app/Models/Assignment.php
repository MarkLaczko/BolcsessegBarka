<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\MorphTo;

class Assignment extends Model
{
    public $timestamps = false;

    public $table = "assignment";

    protected $fillable = [
        "task_name",
        "task",
        "comment",
        "deadline",
        "grade"
    ];

    public function courseable() : MorphTo
    {
        return $this->morphTo();
    }
}

