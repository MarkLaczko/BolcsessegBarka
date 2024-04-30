<?php

namespace App\Http\Resources;

use App\Models\StudentAssignment;
use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class CurrentStudentAssignmentResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        $deadline = Carbon::parse($this->deadline)->timezone('Europe/Budapest')->format('Y.m.d H:i:s');

        return [
            "id" => $this->id,
            "task_name" => $this->task_name,
            "deadline" => $deadline,
            "course_image" => $this->topic->course->image,
            "student_assignment" => StudentAssignmentResource::collection($this->studentAssignment)
        ];
    }
}
