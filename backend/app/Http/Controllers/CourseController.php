<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreCourseRequest;
use App\Http\Resources\CourseResource;
use App\Models\Course;
use Illuminate\Http\Request;

class CourseController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return CourseResource::collection(Course::all());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreCourseRequest $request)
    {
        $data = $request->validated();
        $course = Course::create($data);

        return new CourseResource($course);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $course = Course::findOrFail($id);

        return new CourseResource($course);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(StoreCourseRequest $request, string $id)
    {
        $data = $request->validated();
        $course = Course::findOrFail($id);
        $course->update($data);

        return new CourseResource($course);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $course = Course::findOrFail($id);
        $course->delete();

        return response()->noContent();
    }
}
