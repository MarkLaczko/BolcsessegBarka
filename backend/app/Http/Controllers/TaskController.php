<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreTaskRequest;
use App\Http\Requests\UpdateTaskRequest;
use App\Http\Resources\SubtaskSolutionResource;
use App\Http\Resources\TaskIdResource;
use App\Http\Resources\TaskResource;
use App\Models\Quiz;
use App\Models\Subtask;
use App\Models\Task;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class TaskController extends Controller
{
    public function index(int $id) {
        // The id variable refers to the id property of the given quiz!
        $quiz = Quiz::with(['topic', 'topic.course', 'topic.course.groups' => function($qb) {
                return $qb->with(['users']);
            }])->findOrFail($id);
        Gate::authorize("quiz.tasks.get", $quiz);
        $tasksQB = Task::with(['subtasks' => function ($x){
                $x->orderBy('order');
            }])
            ->where('quiz_id', $id)
            ->orderBy('order');
        return TaskResource::collection($tasksQB->get());
    }

    public function taskIds(int $id) {
        // The id variable refers to the id property of the given quiz!
        $quiz = Quiz::with(['topic', 'topic.course', 'topic.course.groups' => function($qb) {
                return $qb->with(['users']);
            }])->findOrFail($id);
        Gate::authorize("quiz.tasks.get", $quiz);
        $tasksQB = Task::where('quiz_id', $id)
            ->orderBy('order');
        return response([
            'data' => $tasksQB->pluck('id')->all()
        ], 200);
    }

    public function show(int $id) {
        $task = Task::with(['subtasks' => function ($x){
                $x->orderBy('order');
            }])
            ->findOrFail($id);
        Gate::authorize("tasks.get", $task);
        return new TaskResource($task);
    }

    public function solution(int $id) {
        $task = Task::with(['subtasks'])
            ->findOrFail($id);
        Gate::authorize("tasks.get.solutions", $task);
        return SubtaskSolutionResource::collection($task->subtasks);
    }
 
    public function store(StoreTaskRequest $request) {
        $data = $request->validated();

        $quiz = Quiz::findOrFail($data['quiz_id']);
        Gate::authorize("tasks.store", $quiz);
               
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
        
        return new TaskResource(Task::with(['subtasks'])
            ->findOrFail($task->id));
    }

    public function update(UpdateTaskRequest $request, int $id){
        $data = $request->validated();

        $task = Task::findOrFail($id);
        Gate::authorize("tasks.update", $task);

        $task->update([
            'quiz_id' => $data['quiz_id'],
            'order' => $data['order'],
            'header' => $data['header'],
            'text' => $data['text'],
        ]);
        
        foreach ($data['subtasks'] as $key => $value) {
            if(isset($value['options'])) {
                $options_implode = $this::convertArray($value['options']);
                $data['subtasks'][$key]['options'] = $options_implode;
            }

            if(isset($value['solution'])) {
                $solution_implode = $this::convertArray($value['solution']);
                $data['subtasks'][$key]['solution'] = $solution_implode;
            }
        }

        foreach ($data['subtasks'] as $key => $value) {
            if(isset($value['id'])) {
                $subtask = Subtask::findOrFail($value['id']);
                $subtask->update([
                        'task_id' => $id,
                        'order' => $value['order'],
                        'question' => $value['question'],
                        'options' => isset($value['options']) ? $value['options'] : null,
                        'solution' => isset($value['solution']) ? $value['solution'] : null,
                        'type' => $value['type'],
                        'marks' => $value['marks'],
                    ]);
            }
            else {
                Subtask::create([
                        'task_id' => $id,
                        'order' => $value['order'],
                        'question' => $value['question'],
                        'options' => isset($value['options']) ? $value['options'] : null,
                        'solution' => isset($value['solution']) ? $value['solution'] : null,
                        'type' => $value['type'],
                        'marks' => $value['marks'],
                    ]);
            }
        }

        return new TaskResource(Task::with(['subtasks'])
            ->findOrFail($id));
    }

    public function destroy(int $id){
        $task = Task::findOrFail($id);
        Gate::authorize("tasks.delete", $task);

        $task->delete();

        return response()->noContent();
    }

    private static function convertArray($array) : string {
        return '["' . implode('", "', $array) . '"]';
    }

}
