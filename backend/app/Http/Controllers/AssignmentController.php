<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreAssignmentRequest;
use App\Http\Requests\UpdateAssignmentRequest;
use App\Http\Resources\AssignmentResource;
use App\Http\Resources\CurrentStudentAssignmentResource;
use App\Models\Assignment;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\File;
use Illuminate\Support\Facades\Gate;
use Illuminate\Support\Facades\Lang;

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
        Gate::authorize("assignments.store");
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
        Gate::authorize("assignments.update");
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
        Gate::authorize("assignments.destroy");
        $assignment = Assignment::findOrFail($id);
        $assignment->delete();

        return response()->noContent();
    }


    public function download($id) {
        Gate::authorize("assignments.download");
        $item = Assignment::find($id);
    
        if (is_null($item)) {
            return response()->json(['op' => false, 'error' => Lang::get('errors.record_no_found')]);
        }
    
        $file_contents = $item->teacher_task;
        $tempFilePath = tempnam(sys_get_temp_dir(), 'download');
        file_put_contents($tempFilePath, $file_contents);
        
        $mimeType = File::mimeType($tempFilePath);
    
        return response($file_contents)
            ->header('Content-Type', $mimeType)
            ->header('Content-Disposition', 'attachment; filename="' . $item->teacher_task_name . '"')
            ->header('Content-Transfer-Encoding', 'binary');
    }
    
    public function getCurrentAssignments(Request $request) {
        $user = $request->user();
    
        $assignments = Assignment::whereHas('topic.course.groups.users', function($query) use ($user) {
            $query->where('users.id', $user->id);
        })
        ->whereDoesntHave('studentAssignment', function($query) use ($user) {
            $query->where('user_id', $user->id);
        })
        ->with(['studentAssignment' => function($query) use ($user) {
            $query->where('user_id', $user->id);
        }])
        ->where('deadline','>', now())
        ->orderBy('deadline')
        ->get();
    
        return CurrentStudentAssignmentResource::collection($assignments);
    }
}
