<?php

namespace App\Http\Controllers;

use App\Http\Requests\BulkDeleteUserRequest;
use App\Http\Requests\EditProfileRequest;
use App\Http\Requests\StoreUserRequest;
use App\Http\Requests\UpdateUserRequest;
use App\Http\Resources\UserResource;
use App\Models\User;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Gate;

class UserController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        Gate::authorize('users.get');
        return UserResource::collection(User::with(['groups'])->get());
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreUserRequest $request){
        $data = $request->validated();

        User::create([
            'name' => $data['name'],
            'email' => $data['email'],
            'password' => Hash::make($data['password']),
        ]);

        return response()->json([
            "data" => [
                "message" => "Successful registration"
            ]
        ], 200);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        Gate::authorize('users.show');
        $user = User::findOrFail($id);

        return new UserResource($user);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateUserRequest $request, string $id)
    {
        Gate::authorize('users.update');
        $data = $request->validated();
        $user = User::findOrFail($id);
        $user->update($data);

        return new UserResource($user);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        Gate::authorize('users.delete');
        $user = User::findOrFail($id);
        $user->delete();

        return response()->noContent();
    }

    public function bulkDelete(BulkDeleteUserRequest $request)
    {
        Gate::authorize('users.delete');
        $userIds = $request->validated()['userIds'];

        if (!empty($userIds)) {
            User::destroy($userIds);
            return response()->json(['message' => 'Users deleted successfully'], 200);
        }

        return response()->json(['message' => 'No users to delete'], 400);
    }

    public function updateProfile(EditProfileRequest $request)
    {
        $data = $request->validated();

        $user = $request->user();

        if(Hash::check($data['old_password'], $user->password)) {
            $user->update([
                'password' => Hash::make($data['password'])
            ]);

            return response()->json(['message' => 'Profile updated'], 200);
        }

        return response()->json(['message' => 'Incorrect password'], 422);
    }
}
