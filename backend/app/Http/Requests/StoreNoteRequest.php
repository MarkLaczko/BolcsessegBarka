<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Validation\Rule;

class StoreNoteRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules(): array
    {
        return [
            "title" => ["required", "string", "max:40"],
            "text" => ["required"],
            "topic_id" => ["required", "exists:topics,id"],
            "user_id" => ["required", "exists:users,id"],
            "role" => ["required","string",Rule::in(["Tanár","Tanuló"])]
        ];
    }
}
