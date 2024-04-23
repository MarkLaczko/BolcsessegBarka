<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreStudentAssignmentRequest;
use App\Http\Resources\StudentAssignmentResource;
use App\Models\StudentAssignment;
use Illuminate\Http\Request;

class StudentAssignmentController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return StudentAssignmentResource::collection(StudentAssignment::all());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreStudentAssignmentRequest $request)
    {
        $data = $request->validated();
        $assignment = StudentAssignment::create($data);
        $assignment->student_task = $request->file("student_task")->get();
        $assignment->save();
        return new StudentAssignmentResource($assignment);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $assignment = StudentAssignment::findOrFail($id);

        return new StudentAssignmentResource($assignment);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(StoreStudentAssignmentRequest $request, string $id)
    {
        $data = $request->validated();
        $assignment = StudentAssignment::findOrFail($id);
        $assignment->update($data);
        return new StudentAssignmentResource($assignment);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $assignment = StudentAssignment::findOrFail($id);
        $assignment->delete();

        return response()->noContent();
    }

}
