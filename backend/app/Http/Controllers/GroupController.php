<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreGroupRequest;
use App\Http\Requests\UpdateGroupRequest;
use App\Http\Resources\GroupResource;
use App\Models\Group;
use Illuminate\Http\Request;

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
        $data = $request->validated();
        $group = Group::findOrFail($id);
        $group->update([
            'name' => $data['name']
        ]);

        if(!empty($data['selectedUsers'])){
            $users = [];
            foreach($data['selectedUsers'] as $user){
                $users[$user['id']] = ['permission_id' => $user['permission_id']];
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
        $group = Group::findOrFail($id);
        $group->delete();

        return response()->noContent();
    }
}
