<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateAssignmentRequest extends FormRequest
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
            'task_name' => 'nullable|string',
            'teacher_task' => 'nullable',
            'comment' => 'nullable|string|max:255',
            'deadline' => 'nullable|date',
            'teacher_task_name' => 'nullable|string',
        ];
    }
}
