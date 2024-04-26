<?php

namespace App\Http\Resources;

use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class QuizResource extends JsonResource
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
            'name' => $this->name,
            'max_attempts' => $this->max_attempts,
            'opens' => $this->opens == null ? null : Carbon::parse($this->opens)->timestamp,
            'closes' => $this->closes == null ? null : Carbon::parse($this->closes)->timestamp,
            'time' => $this->time,
            'number_of_tasks' => $this->tasks_count(),
            'tasks' => TaskResource::collection($this->whenLoaded('tasks')),
            'topic' => new TopicResource($this->whenLoaded('topic')),
        ];
    }
}
