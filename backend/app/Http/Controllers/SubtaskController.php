<?php

namespace App\Http\Controllers;

use App\Http\Resources\SubtaskSolutionResource;
use App\Models\Subtask;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class SubtaskController extends Controller
{
    public function destroy(int $id){
        $subtask = Subtask::findOrFail($id);
        Gate::authorize("subtasks.delete", $subtask);
        
        $subtask->delete();

        return response()->noContent();
    }
}
