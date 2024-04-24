<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreQuizRequest;
use App\Http\Requests\UpdateQuizRequest;
use App\Http\Resources\QuizResource;
use App\Models\Quiz;
use Carbon\Carbon;
use Illuminate\Http\Request;

class QuizController extends Controller
{
    public function index() {
        return QuizResource::collection(Quiz::all());
    }

    public function show(int $id) {
        return new QuizResource(Quiz::findOrFail($id));
    }

    public function store(StoreQuizRequest $request) {
        $data = $request->validated();

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
        $quiz = Quiz::findOrFail($id);
        $quiz->delete();

        return response()->noContent();
    }
}
