<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class AnswerResoruce extends JsonResource
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
            'attempt' => new AttemptResoruce($this->whenLoaded('attempt')),
            'subtask' => new SubtaskResource($this->whenLoaded('subtask')),
            'answer' => $this->answer,
            'correct' => $this->correct,
            'marks' => $this->marks,
        ];
    }
}
