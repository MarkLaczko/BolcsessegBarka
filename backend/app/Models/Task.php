<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Task extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "question",
        "asnwer",
        "type"
    ];

    public function quizzes() : BelongsToMany
    {
        return $this->belongsToMany(Quiz::class,"solve","task_id","quiz_id");
    }
}
