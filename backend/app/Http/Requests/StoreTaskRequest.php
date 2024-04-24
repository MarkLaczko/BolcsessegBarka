<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreTaskRequest extends FormRequest
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
            'quiz_id' => ['required', 'numeric', 'integer', 'exists:quizzes,id'],
            'order' => ['required', 'numeric', 'integer', 'min:0'],
            'header' => ['required', 'string', 'max:100'],
            'text' => ['required', 'string', 'max:255'],
        ];
    }
}
