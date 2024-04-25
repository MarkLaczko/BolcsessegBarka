<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class SubtaskResource extends JsonResource
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
            'question' => $this->question,
            'options' => isset($this->options) ? json_decode($this->options, true) : null,
            'type' => $this->type,
            'marks' => $this->marks,
            'task' =>new TaskResource($this->whenLoaded('task')),
        ];
    }
}
