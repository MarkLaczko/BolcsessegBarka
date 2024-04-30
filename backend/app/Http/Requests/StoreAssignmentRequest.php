<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Validation\Rule;

class StoreAssignmentRequest extends FormRequest
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
            'task_name' => 'required|string',
            'teacher_task' => 'nullable',
            'comment' => 'nullable|string|max:255',
            'deadline' => 'required|date',
            'courseable_id' => ['required', Rule::exists("courses","id")],
            'teacher_task_name' => 'nullable|string',
            'topic_id' => ['required', Rule::exists("topics","id")]
        ];
    }
}
