<?php

namespace App\Http\Resources;

use App\Models\Assignment;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class AssignmentResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            'task_name' => $this->task_name,
            'comment' => $this->comment,
            'deadline' => $this->deadline,
            'grade' => $this->grade,
            'course' => $this->courseable,
            'teacher_task_name' => $this->teacher_task_name,
            'student_task' => $this->studentAssignment,
        ];
    }
}
