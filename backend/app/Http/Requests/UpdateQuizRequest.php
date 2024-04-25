<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateQuizRequest extends FormRequest
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
            'name' => ['required', 'max:100'],
            'max_attempts' => ['nullable', 'numeric', 'integer', 'min:0'],
            'opens' => ['nullable', 'numeric', 'integer', 'min:0'],
            'closes' => ['nullable', 'numeric', 'integer', 'min:0'],
            'time' => ['nullable', 'numeric', 'integer', 'min:0'],
            'topic_id' => ['nullable', 'numeric', 'integer', 'exists:topics,id'],
        ];
    }
}
