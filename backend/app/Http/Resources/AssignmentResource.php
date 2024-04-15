<?php

namespace App\Http\Resources;

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
            'task' => $this->task,
            'comment' => $this->comment,
            'deadline' => $this->deadline,
            'grade' => $this->grade,
            'course' => $this->courseable
        ];
    }
}
