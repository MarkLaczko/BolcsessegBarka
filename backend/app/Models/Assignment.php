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
        "grade",
        "courseable_id",
        "courseable_type",
        "student_task_name" ,
        "teacher_task_name"
    ];

    public function courseable() : MorphTo
    {
        return $this->morphTo();
    }

    public function studentAssignment() : HasMany
    {
        return $this->hasMany(StudentAssignment::class)->select(["assignment_id","student_task_name"]);
    }

}

