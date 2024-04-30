<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class StudentAssignment extends Model
{
    use HasFactory;

    public $timestamps = false;

    protected $fillable = [
        "assignment_id",
        "student_task_name" ,
        "student_task",
        "user_id",
        "grade",
    ];

    public function assignment() : BelongsTo
    {
        return $this->belongsTo(Assignment::class);
    }

    public function user() : BelongsTo
    {
        return $this->belongsTo(User::class);
    }
}
