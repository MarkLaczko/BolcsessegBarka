<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreAnswerRequest extends FormRequest
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
            'attempt_id' => ['required', 'numeric', 'exists:attempts,id'],
            "subtask_id" => ['required', 'numeric', 'exists:subtasks,id'],
            'answer' => ['required', 'string', 'max:16000']
        ];
    }
}
