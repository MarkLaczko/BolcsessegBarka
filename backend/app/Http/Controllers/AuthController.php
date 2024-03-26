<?php

namespace App\Http\Controllers;

use App\Http\Requests\LoginRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class AuthController extends Controller
{
    public function authenticate(LoginRequest $request){
        $data = $request->validated();

        if(Auth::attempt($data)){
            $token = $request->user()->createToken('token');

            return response()->json([
                "data" => [
                    "token" => $token->plainTextToken
                ]
            ], 200);
        }

        return response()->json([
            "message" => [
                "message" => "Unsuccessful authentication"
            ]
        ], 401);
    }
}
