<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreAttemptRequest;
use App\Http\Requests\UpdateAttemptRequest;
use App\Http\Resources\AttemptResoruce;
use App\Models\Attempt;
use App\Models\Quiz;
use Carbon\Carbon;
use Illuminate\Http\Request;

class AttemptController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return AttemptResoruce::collection(Attempt::with(['quiz', 'user'])->get());
    }

    public function quizAttempts(int $id)
    {
        // The id variable refers to the id property of the given quiz!
        $quiz = Quiz::findOrFail($id);

        $attempts = Attempt::with(['user'])
            ->where('quiz_id', $id)
            ->get();

        return AttemptResoruce::collection($attempts);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreAttemptRequest $request)
    {
        $data = $request->validated();
        $quiz = Quiz::findOrFail($data['quiz_id']);
        
        $attempt = Attempt::create([
            'quiz_id' => $data['quiz_id'],
            'user_id' => $request->user()->id,
            'start' => Carbon::now(),
        ]);

        return new AttemptResoruce(Attempt::with(['quiz'])->findOrFail($attempt->id));
    }

    /**
     * Display the specified resource.
     */
    public function show(int $id)
    {
        $attempt = Attempt::with(['quiz', 'user'])->findOrFail($id);

        return new AttemptResoruce($attempt);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAttemptRequest $request, int $id)
    {
        $data = $request->validated();
        $attempt = Attempt::with(['quiz', 'user'])->withSum('answers', 'marks')->findOrFail($id);


        $attempt->update([
            'marks' => isset($data['marks']) ? $data['marks'] : $attempt->marks,
            'grade' => $data['grade']
        ]);

        return new AttemptResoruce($attempt);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(int $id)
    {
        $attempt = Attempt::with(['quiz', 'user'])->findOrFail($id);

        $attempt->delete();

        return response()->noContent();
    }

    public function finish(int $id)
    {
        $attempt = Attempt::with(['quiz', 'user'])->findOrFail($id);

        $attempt->update([
            'end' => Carbon::now()
        ]);

        return new AttemptResoruce($attempt);
    }
}
