<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
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
        "courseable_id",
        "teacher_task_name",
        "topic_id"
    ];

    public function topic() : BelongsTo
    {
        return $this->belongsTo(Topic::class);
    }

    public function studentAssignment() : HasMany
    {
        return $this->hasMany(StudentAssignment::class)->select(["assignment_id","student_task_name"]);
    }

}

