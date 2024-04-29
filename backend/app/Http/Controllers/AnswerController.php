<?php

namespace App\Http\Controllers;

use App\Http\Requests\BulkStoreAnswerRequest;
use App\Http\Requests\StoreAnswerRequest;
use App\Http\Requests\UpdateAnswerRequest;
use App\Http\Resources\AnswerResoruce;
use App\Models\Answer;
use App\Models\Attempt;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class AnswerController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        Gate::authorize('answers.index');
        return AnswerResoruce::collection(Answer::with(['attempt', 'attempt.user', 'attempt.quiz', 'subtask', 'subtask.task',])->get());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreAnswerRequest $request)
    {
        $data = $request->validated();
        $attempt = Attempt::findOrFail($data['attempt_id']);
        Gate::authorize('answers.store', $attempt);
        $answer = Answer::create($data);
        return new AnswerResoruce($answer);
    }

    public function bulkStore(BulkStoreAnswerRequest $request)
    {
        $data = $request->validated();

        $attempt = Attempt::findOrFail($data['attempt_id']);
        Gate::authorize('answers.store', $attempt);

        foreach ($data['bulk'] as $value) {
            Answer::create([
                'attempt_id' => $data['attempt_id'],
                'subtask_id' => $value['subtask_id'],
                'answer' => $value['answer'],
            ]);
        }

        return response()->json([
            'data' => [
                'message' => 'Answers successfully created.'
            ]
        ], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(int $id)
    {
        $answer = Answer::with(['attempt', 'attempt.user', 'attempt.quiz', 'subtask', 'subtask.task',])->findOrFail($id);
        Gate::authorize('answers.store', $answer);

        return new AnswerResoruce($answer);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAnswerRequest $request, int $id)
    {
        $data = $request->validated();
        $answer = Answer::findOrFail($id);
        Gate::authorize('answers.update', $answer);
        $answer->update($data);

        return new AnswerResoruce($answer);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(int $id)
    {
        $answer = Answer::findOrFail($id);
        Gate::authorize('answers.destroy', $answer);
        $answer->delete();

        return response()->noContent();
    }
}
