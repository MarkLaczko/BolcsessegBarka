<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreAssignmentRequest;
use App\Http\Requests\UpdateAssignmentRequest;
use App\Http\Resources\AssignmentResource;
use App\Models\Assignment;
use Illuminate\Http\Request;

class AssignmentController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return AssignmentResource::collection(Assignment::all());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreAssignmentRequest $request)
    {
        $data = $request->validated();
        $assignment = Assignment::create($data);
        if ($request->file("teacher_task") != null) {
            $assignment->teacher_task = $request->file("teacher_task")->get();
            $assignment->save();
        }
        return new AssignmentResource($assignment);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $assignment = Assignment::findOrFail($id);

        return new AssignmentResource($assignment);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAssignmentRequest $request, string $id)
    {
        $data = $request->validated();
        $assignment = Assignment::findOrFail($id);
        $assignment->update($data);
        if ($request->file("teacher_task") != null) {
            $assignment->teacher_task = $request->file("teacher_task")->get();
            $assignment->save();
        }
        return new AssignmentResource($assignment);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $assignment = Assignment::findOrFail($id);
        $assignment->delete();

        return response()->noContent();
    }

}
