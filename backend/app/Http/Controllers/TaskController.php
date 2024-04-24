<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreTaskRequest;
use App\Http\Resources\TaskIdResource;
use App\Http\Resources\TaskResource;
use App\Models\Quiz;
use App\Models\Subtask;
use App\Models\Task;
use Illuminate\Http\Request;

class TaskController extends Controller
{
    public function index(int $id) {
        // The id variable refers to the id property of the given quiz!
        $tasksQB = Task::with(['subtasks'])
            ->where('quiz_id', $id);
        if($tasksQB->exists()){
            return TaskResource::collection($tasksQB->get());
        }
        return abort(404, "No query results for model [App\\Models\\Quiz] {$id}");
    }

    public function taskIds(int $id) {
        // The id variable refers to the id property of the given quiz!
        $tasksQB = Task::where('quiz_id', $id);
        if($tasksQB->exists()){
            return response([
                'data' => $tasksQB->pluck('id')->all()
            ], 200);
        }
        return abort(404, "No query results for model [App\\Models\\Quiz] {$id}");
    }

    public function show(int $id) {
        $task = Task::with(['subtasks'])
            ->findOrFail($id);
        return new TaskResource($task);
    }
 
    public function store(StoreTaskRequest $request) {
        $data = $request->validated();
               
        $task = Task::create([
            'quiz_id' => $data['quiz_id'],
            'order' => $data['order'],
            'header' => $data['header'],
            'text' => $data['text'],
        ]);

        foreach ($data['subtasks'] as $key => $value) {
            if(isset($value['options'])){
                $options_implode = $this::convertArray($value['options']);
                $data['subtasks'][$key]['options'] = $options_implode;
            }

            if(isset($value['solution'])){
                $solution_implode = $this::convertArray($value['solution']);
                $data['subtasks'][$key]['solution'] = $solution_implode;
            }
        }

        foreach ($data['subtasks'] as $key => $value) {
            Subtask::create([
                'task_id' => $task->id,
                'order' => $value['order'],
                'question' => $value['question'],
                'options' => isset($value['options']) ? $value['options'] : null,
                'solution' => isset($value['solution']) ? $value['solution'] : null,
                'type' => $value['type'],
                'marks' => $value['marks'],
            ]);
        }
        
        return Task::with(['subtasks'])
            ->findOrFail($task->id);
    }

    private static function convertArray($array) : string {
        return '["' . implode('", "', $array) . '"]';
    }

}
