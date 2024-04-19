<?php

namespace App\Http\Controllers;

use App\Http\Requests\DeleteCoursesRequest;
use App\Http\Requests\StoreCourseRequest;
use App\Http\Requests\StoreGroupIdRequest;
use App\Http\Requests\UpdateCourseRequest;
use App\Http\Resources\CourseResource;
use App\Http\Resources\GroupsWithUsersResource;
use App\Models\Course;
use App\Models\Topic;
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
        $course = Course::with("topics")->findOrFail($id);

     return new CourseResource($course);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateCourseRequest $request, string $id)
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
        $course->groups()->detach();
        $course->delete();

        return response()->noContent();
    }

    public function bulkDelete(DeleteCoursesRequest $request)
    {
        $courseIds = $request->validated()["courseIds"];

         $courses = Course::whereIn('id', $courseIds)->get();

         foreach ($courses as $course) {
             $course->groups()->detach();
         }

        if (!empty($courseIds)) {
            Course::destroy($courseIds);
            return response()->json(['message' => 'Courses deleted successfully'], 200);
        }

        return response()->json(['message' => 'No Courses to delete'], 400);
    }

    public function assignGroups(StoreGroupIdRequest $request, Course $course)
    {
        $data = $request->validated();

        $course->groups()->sync($data['group_ids']);

        return response()->json([
            'message' => 'Groups successfully assigned to the course.',
            'course' => $course->load('groups')
        ]);
    }

    public function assignTopics(Request $request, $courseId)
    {
        $course = Course::find($courseId);
        if (!$course) {
            return response()->json(['message' => 'Course not found'], 404);
        }
    
        $topicIds = $request->input('topic_ids');
        foreach ($topicIds as $topicId) {
            $topic = Topic::find($topicId);
            if ($topic) {
                $topic->course_id = $courseId;
                $topic->save();
            }
        }
    
        $course->load('topics');
        return new CourseResource($course);
    }

    public function showCourseWithUsers($id)
    {
        $course = Course::with(['groups', 'groups.users'])->findOrFail($id);
        
        return new GroupsWithUsersResource($course);
    }
}
