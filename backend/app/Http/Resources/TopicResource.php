<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class TopicResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            "id" => $this->id,
            "name" => $this->name,
            "course" => new CourseResource($this->whenLoaded('course')),
            "assignment" => AssignmentResource::collection($this->whenLoaded('assignments')),
            'quizzes' => QuizResource::collection($this->whenLoaded('quizzes')),
        ];
    }
}
