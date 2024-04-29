<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreTopicRequest;
use App\Http\Requests\UpdateTopicRequest;
use App\Http\Resources\TopicResource;
use App\Models\Topic;
use Illuminate\Support\Facades\Gate;

class TopicController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return TopicResource::collection(Topic::with(['assignments', 'notes', 'quizzes'])->get());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreTopicRequest $request)
    {
        Gate::authorize("topics.store");

        $data = $request->validated();
        $topic = Topic::create($data);

        return new TopicResource($topic); 
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $topic = Topic::with(['assignments', 'notes', 'quizzes'])->findOrFail($id);

        return new TopicResource($topic);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateTopicRequest $request, string $id)
    {
        Gate::authorize("topics.update");

        $data = $request->validated();
        $topic = Topic::with(['assignments', 'notes', 'quizzes'])->findOrFail($id);
        $topic->update($data);

        return new TopicResource($topic);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        Gate::authorize("topics.destroy");

        $topic = Topic::findOrFail($id);
        $topic->delete();

        return response()->noContent();
    }
}
