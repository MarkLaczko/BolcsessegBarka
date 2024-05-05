<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreStudentAssignmentRequest;
use App\Http\Resources\StudentAssignmentResource;
use App\Models\Assignment;
use App\Models\StudentAssignment;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\File;
use Illuminate\Support\Facades\Lang;
use League\CommonMark\Node\Block\Document;
use ZipArchive;

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
        $assignment->student_task = $request->file("student_task")->get();
        $assignment->save();
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

    public function download($id) {
        $item = StudentAssignment::find($id);
    
        if (is_null($item)) {
            return response()->json(['op' => false, 'error' => Lang::get('errors.record_no_found')]);
        }
    
        $file_contents = $item->student_task;
        $tempFilePath = tempnam(sys_get_temp_dir(), 'download');
        file_put_contents($tempFilePath, $file_contents);
        
        $mimeType = File::mimeType($tempFilePath);
    
        return response($file_contents)
            ->header('Content-Type', $mimeType)
            ->header('Content-Disposition', 'attachment; filename="' . $item->student_task_name . '"')
            ->header('Content-Transfer-Encoding', 'binary');
    }

    public function createZip($id, $filename = 'Assignment.zip')
    {
        $item = Assignment::with("studentAssignment")->findOrFail($id);

        $items = $item->studentAssignmentsWithTask;

        $zip = new ZipArchive;
        $zipFileName = $filename;

        if ($zip->open(public_path($zipFileName), ZipArchive::CREATE) === TRUE) {

            foreach ($items as $file) {

                $file_contents = $file->student_task;
                $tempFilePath = tempnam(sys_get_temp_dir(), 'download');
                file_put_contents($tempFilePath, $file_contents);

                $zip->addFile($tempFilePath, $file->student_task_name);
            }

            $zip->close();

            return response()->download(public_path($zipFileName))->deleteFileAfterSend(true);
        } else {
            return "Failed to create the zip file.";
        }
    }

}
