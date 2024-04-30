<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreAttemptRequest;
use App\Http\Requests\UpdateAttemptRequest;
use App\Http\Resources\AttemptQuizDetailsResource;
use App\Http\Resources\AttemptResoruce;
use App\Models\Attempt;
use App\Models\Quiz;
use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class AttemptController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        Gate::authorize('attempts.index');
        return AttemptResoruce::collection(Attempt::with(['quiz', 'user'])->get());
    }

    public function quizAttempts(int $id)
    {
        // The id variable refers to the id property of the given quiz!
        $quiz = Quiz::findOrFail($id);
        Gate::authorize('attempts.quizAttempts', $quiz);
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
        Gate::authorize('attempts.store', $quiz);

        $userAttempts = Attempt::where('quiz_id', $data['quiz_id'])->count();
        if(isset($quiz->max_attempts) && $userAttempts >= $quiz->max_attempts){
            abort(403);
        }

        $attempt = Attempt::create([
            'quiz_id' => $data['quiz_id'],
            'user_id' => $request->user()->id,
            'start' => Carbon::now(),
        ]);

        return new AttemptResoruce(Attempt::with(['quiz', 'user'])->findOrFail($attempt->id));
    }

    /**
     * Display the specified resource.
     */
    public function show(int $id)
    {
        $attempt = Attempt::with(['quiz', 'quiz.tasks.subtasks', 'quiz.topic.course.groups.users', 'user', 'answers', 'answers.subtask'])->findOrFail($id);
        Gate::authorize('attempts.show', $attempt);
        return new AttemptResoruce($attempt);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAttemptRequest $request, int $id)
    {
        $data = $request->validated();
        $attempt = Attempt::with(['quiz', 'user'])->withSum('answers', 'marks')->findOrFail($id);
        Gate::authorize('attempts.update', $attempt);

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
        Gate::authorize('attempts.destroy', $attempt);

        $attempt->delete();

        return response()->noContent();
    }

    public function finish(int $id)
    {
        $attempt = Attempt::with(['quiz', 'user'])->findOrFail($id);
        Gate::authorize('attempts.finish', $attempt);
        $attempt->update([
            'end' => Carbon::now()
        ]);

        return new AttemptResoruce($attempt);
    }

    public function userAttempts(Request $request)
    {
        $user = $request->user();
        $attempts = Attempt::with(['quiz', 'quiz.tasks', 'quiz.tasks.subtasks', 'user'])
            ->where('user_id', $user->id)
            ->get();
        return AttemptResoruce::collection($attempts);
    }

    public function getAttemptQuizDetails(int $id)
    {
        $attempt = Attempt::with(['quiz', 'quiz.topic.course.groups.users'])->findOrFail($id);
        return new AttemptQuizDetailsResource($attempt->quiz->topic->course);
    }
}
