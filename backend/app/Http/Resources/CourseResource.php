<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class CourseResource extends JsonResource
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
            "image" => $this->image,
            'topics' => TopicResource::collection($this->whenLoaded('topics')),
            "groups" => GroupResource::collection($this->whenLoaded('groups')),
            "created_by" => $this->created_by
        ];
    }
}
