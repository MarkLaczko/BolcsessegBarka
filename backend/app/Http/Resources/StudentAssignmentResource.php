<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class StudentAssignmentResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            "assignment_id" => $this->assignment_id,
            "student_task_name" => $this->student_task_name,
            'grade' => $this->grade,
            'user_id' => $this->user_id,
        ];
    }
}
