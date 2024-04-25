<?php

namespace App\Http\Controllers;

use App\Http\Resources\SubtaskSolutionResource;
use App\Models\Subtask;
use Illuminate\Http\Request;

class SubtaskController extends Controller
{
    public function solution(int $id){
        $subtask = Subtask::findOrFail($id);

        return new SubtaskSolutionResource($subtask);
    }

    public function destroy(int $id){
        $subtask = Subtask::findOrFail($id);
        
        $subtask->delete();

        return response()->noContent();
    }
}
