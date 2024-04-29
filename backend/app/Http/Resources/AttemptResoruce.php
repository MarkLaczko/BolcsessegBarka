<?php

namespace App\Http\Resources;

use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class AttemptResoruce extends JsonResource
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
            'quiz' => new QuizResource($this->whenLoaded('quiz')),
            'user' => new UserResource($this->whenLoaded('user')),
            'start' => $this->start == null ? null : Carbon::parse($this->start)->timestamp,
            'end' => $this->end == null ? null : Carbon::parse($this->end)->timestamp,
            'answers' => AnswerResoruce::collection($this->whenLoaded('answers')),
            'marks' => $this->marks,
            'grade' => $this->grade,
        ];
    }
}
