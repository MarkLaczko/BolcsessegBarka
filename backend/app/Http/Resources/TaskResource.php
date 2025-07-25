<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class TaskResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            'id' => $this->id,
            'order' => $this->order,
            'header' => $this->header,
            'text' => $this->text,
            'subtasks' => SubtaskResource::collection($this->subtasks),
            'quiz' => new QuizResource($this->whenLoaded('quiz')),
        ];
    }
}
