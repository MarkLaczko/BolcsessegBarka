<?php

namespace App\Http\Controllers;

use App\Http\Requests\BulkDeleteGroupRequest;
use App\Http\Requests\StoreGroupRequest;
use App\Http\Requests\UpdateGroupRequest;
use App\Http\Resources\GroupResource;
use App\Models\Group;
use Exception;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Gate;

class GroupController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return GroupResource::collection(Group::with(['users', 'courses'])->get());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreGroupRequest $request)
    {
        Gate::authorize('groups.store');
        $data = $request->validated();
        $group = Group::create($data);

        return new GroupResource(Group::with(['users', 'courses'])->findOrFail($group->id));
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $group = Group::with(['users', 'courses'])->findOrFail($id);

        return new GroupResource($group);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateGroupRequest $request, string $id)
    {
        Gate::authorize('groups.update');
        $data = $request->validated();
        $group = Group::with(['users'])->findOrFail($id);
        $group->update([
            'name' => $data['name']
        ]);

        $group->users()->detach($group->users);

        if(!empty($data['selectedUsers'])){
            $users = [];
            foreach($data['selectedUsers'] as $user){
                $users[$user['id']] = ['permission' => $user['permission']];
            }
            $group->users()->attach($users);
        }

        return new GroupResource(Group::with(['users', 'courses'])->findOrFail($group->id));
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        Gate::authorize('groups.delete');
        $group = Group::findOrFail($id);
        $group->users()->detach();
        $group->courses()->detach();
        $group->delete();

        return response()->noContent();
    }

    /**
     * Remove a listing of resources from storage.
     */
    public function bulkDelete(BulkDeleteGroupRequest $request)
    {
        Gate::authorize('groups.delete');
        $data = $request->validated();
        try {
            $groups = Group::whereIn('id', $data['bulk'])
                ->get();
            
            foreach ($groups as $group) {
                $group->users()->detach();
                $group->courses()->detach();
                $group->delete();
            }

            return response()->json(['message' => 'Groups deleted successfully'], 200);
        } catch (Exception $e) {
            return response()->json(['message' => $e->getMessage()], 500);
        }
    }
}
