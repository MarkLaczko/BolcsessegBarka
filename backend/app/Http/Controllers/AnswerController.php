<?php

namespace App\Http\Controllers;

use App\Http\Resources\AnswerResoruce;
use App\Models\Answer;
use Illuminate\Http\Request;

class AnswerController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return AnswerResoruce::collection(Answer::with(['attempt', 'attempt.user', 'attempt.quiz', 'subtask', 'subtask.task',])->get());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(int $id)
    {
        $answer = Answer::with(['attempt', 'attempt.user', 'attempt.quiz', 'subtask', 'subtask.task',])->findOrFail($id);

        return new AnswerResoruce($answer);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        //
    }
}
