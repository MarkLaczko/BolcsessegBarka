<?php

namespace App\Http\Controllers;

use App\Http\Resources\SubtaskSolutionResource;
use App\Models\Subtask;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class SubtaskController extends Controller
{
    public function solution(int $id){
        $subtask = Subtask::findOrFail($id);
        Gate::authorize("subtasks.solutions", $subtask);

        return new SubtaskSolutionResource($subtask);
    }

    public function destroy(int $id){
        $subtask = Subtask::findOrFail($id);
        Gate::authorize("tasks.delete", $subtask);
        
        $subtask->delete();

        return response()->noContent();
    }
}
