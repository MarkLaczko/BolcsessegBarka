<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreNoteRequest;
use App\Http\Requests\UpdateNoteRequest;
use App\Http\Resources\NoteResource;
use App\Models\Course;
use App\Models\Note;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class NoteController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return NoteResource::collection(Note::all());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreNoteRequest $request)
    {
        $data = $request->validated();
        $note = Note::create($data);

        return new NoteResource($note);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $note = Note::findOrFail($id);

        return new NoteResource($note);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateNoteRequest $request, string $id)
    {
        $note = Note::findOrFail($id);
        Gate::authorize("notes.update", $note);

        $data = $request->validated();
        $note->update($data);

        return new NoteResource($note);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $note = Note::findOrFail($id);
        Gate::authorize("notes.destroy", $note);

        $note->delete();

        return response()->noContent();
    }

    public function getCurrentNotes(Request $request) {
        $user = $request->user();

        $notes = Note::with(["topic","topic.course","topic.course.groups" => function($qb) use ($user) {
            return $qb->with(["users" => function ($qb2) use ($user) {
                return $qb2->where("id",$user->id);
            }]);    
        }])->with(["user" => function ($qb3) {
            return $qb3->with("groups");
        }]);

        Gate::authorize("notes.getCurrentNotes", $notes);

        $returnNotes = [];

        foreach ($notes->get() as $value) {
           if ($user->id == $value["user_id"]) {
            array_push($returnNotes, $value);
           }
           elseif($value["role"] == "Tanár") {
            array_push($returnNotes, $value);
           }
        }

        return NoteResource::collection(collect($returnNotes));
    }
}
