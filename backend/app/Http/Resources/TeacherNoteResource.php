<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class TeacherNoteResource extends JsonResource
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
            "title" => $this->title,
            "text" => $this->text,
            "updated_at" => $this->updated_at->timezone('Europe/Budapest')->format('Y-m-d H:i:s'),
            "course_name" => $this->topic->course->name,
            "course_image" => $this->topic->course->image
        ];
    }
}
