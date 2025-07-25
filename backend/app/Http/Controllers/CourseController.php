<?php

namespace App\Http\Controllers;

use App\Http\Requests\DeleteCoursesRequest;
use App\Http\Requests\StoreCourseRequest;
use App\Http\Requests\StoreGroupIdRequest;
use App\Http\Requests\StoreTopicIdRequest;
use App\Http\Requests\UpdateCourseRequest;
use App\Http\Resources\CourseResource;
use App\Http\Resources\GroupsWithUsersResource;
use App\Models\Course;
use App\Models\Topic;
use Illuminate\Support\Facades\Gate;

class CourseController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return CourseResource::collection(Course::with(["groups"])->get());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreCourseRequest $request)
    {
        Gate::authorize('courses.store');

        $data = $request->validated();
        $data['created_by'] = $request->user()->id;

        $course = Course::create($data);

        return new CourseResource($course);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $course = Course::with(["topics", 'topics.assignments', 'topics.notes', 'topics.quizzes',"groups"])
            ->findOrFail($id);

        return new CourseResource($course);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateCourseRequest $request, string $id)
    {
        $course = Course::findOrFail($id);
        Gate::authorize('courses.update', $course);

        $data = $request->validated();
        $course->update($data);

        return new CourseResource($course);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $course = Course::findOrFail($id);
        Gate::authorize('courses.destroy', $course);

        $course->groups()->detach();
        $course->delete();

        return response()->noContent();
    }

    public function bulkDelete(DeleteCoursesRequest $request)
    {
        $courseIds = $request->validated()["courseIds"];

        $courses = Course::whereIn('id', $courseIds)->get();

        foreach ($courses as $course) {
            Gate::authorize('courses.destroy', $course);
        }

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
        Gate::authorize("courses.assignGroups");
        $data = $request->validated();

        if (!empty($data['group_ids'])) {
            $course->groups()->sync($data['group_ids']);
        } else {
            $course->groups()->detach();
        }

        return response()->json([
            'message' => 'Groups successfully assigned to the course.',
            'course' => $course->load('groups')
        ]);
    }

    public function assignTopics(StoreTopicIdRequest $request, $courseId)
    {
        Gate::authorize("courses.assignTopics");
        $course = Course::find($courseId);

        if (!$course) {
            return response()->json(['message' => 'Course not found'], 404);
        }

        $topicIds = $request->validated()['topic_ids'];
        foreach ($topicIds as $topicId) {
            $topic = Topic::find($topicId);
            if ($topic) {
                $topic->course_id = $courseId;
                $topic->save();
            }
        }

        $course = Course::with(["topics" => function($query) {
            return $query->with(['assignments', 'notes', 'quizzes']);
        }])->find($courseId);
        return new CourseResource($course);
    }

    public function showCourseWithUsers($id)
    {
        $course = Course::with(['groups', 'groups.users'])->findOrFail($id);

        return new GroupsWithUsersResource($course);
    }
}
