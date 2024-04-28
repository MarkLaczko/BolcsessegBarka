<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreQuizRequest;
use App\Http\Requests\UpdateQuizRequest;
use App\Http\Resources\QuizResource;
use App\Models\Quiz;
use App\Models\Topic;
use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class QuizController extends Controller
{
    public function index() {
        Gate::authorize("quizzes.getAll");
        return QuizResource::collection(Quiz::all());
    }

    public function show(int $id) {
        $quiz = Quiz::with(['topic', 'topic.course', 'topic.course.groups' => function($qb) {
                return $qb->with(['users']);
            }])->findOrFail($id);
        Gate::authorize("quizzes.get", $quiz);
        return new QuizResource($quiz);
    }

    public function store(StoreQuizRequest $request) {
        $data = $request->validated();
        $course = Topic::with(['course'])->findOrFail($data['topic_id'])->course;
        Gate::authorize("quizzes.store", $course);

        if(!empty($data['opens'])) {
            $data['opens'] = Carbon::createFromTimestamp($data['opens'])->toDateTimeString();
        }
        if(!empty($data['closes'])) {
            $data['closes'] = Carbon::createFromTimestamp($data['closes'])->toDateTimeString();
        }

        $quiz = Quiz::create($data);

        return new QuizResource($quiz);
    }

    public function update(UpdateQuizRequest $request, int $id) {
        $data = $request->validated();
        $course = Topic::with(['course'])->findOrFail($data['topic_id'])->course;
        Gate::authorize("quizzes.update", $course);

        if(!empty($data['opens'])) {
            $data['opens'] = Carbon::createFromTimestamp($data['opens'])->toDateTimeString();
        }
        if(!empty($data['closes'])) {
            $data['closes'] = Carbon::createFromTimestamp($data['closes'])->toDateTimeString();
        }

        $quiz = Quiz::findOrFail($id);
        $quiz->update($data);

        return new QuizResource($quiz);
    }

    public function destroy(int $id) {
        $quiz = Quiz::with(['topic', 'topic.course', 'topic.course.groups' => function($qb) {
                return $qb->with(['users']);
            }])->findOrFail($id);
        Gate::authorize("quizzes.destroy", $quiz);
        $quiz->delete();

        return response()->noContent();
    }
}
