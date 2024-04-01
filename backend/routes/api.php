<?php

use App\Http\Controllers\AuthController;
use App\Http\Controllers\CourseController;
use App\Http\Controllers\GroupController;
use App\Http\Controllers\UserController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::get('/users', [UserController::class, 'index'])
    ->name('users.index');
Route::get('/users/{id}', [UserController::class, 'show'])
    ->name('users.show');
Route::put('/users/{id}', [UserController::class, 'update'])
    ->name('users.update');
Route::delete('/users/{id}', [UserController::class, 'destroy'])
    ->name('users.destroy');

Route::post('/users/register', [UserController::class, 'store'])
    ->name('users.store');
Route::post('/users/login', [AuthController::class, 'authenticate'])
    ->name('users.login');

Route::middleware('auth:sanctum')->get('/courses', [CourseController::class, 'index'])
    ->name('courses.index');
Route::middleware('auth:sanctum')->get('/courses/{id}', [CourseController::class, 'show'])
    ->name('courses.show');
Route::middleware('auth:sanctum')->post('/courses', [CourseController::class, 'store'])
    ->name('courses.store');
Route::middleware('auth:sanctum')->put('/courses/{id}', [CourseController::class, 'update'])
    ->name('courses.update');
Route::middleware('auth:sanctum')->delete('/courses/{id}', [CourseController::class, 'destroy'])
    ->name('courses.destroy');

Route::middleware('auth:sanctum')->get('/groups', [GroupController::class, 'index'])
    ->name('groups.index');
Route::middleware('auth:sanctum')->get('/groups/{id}', [GroupController::class, 'show'])
    ->name('groups.show');
Route::middleware('auth:sanctum')->post('/groups', [GroupController::class, 'store'])
    ->name('groups.store');
Route::middleware('auth:sanctum')->put('/groups/{id}', [GroupController::class, 'update'])
    ->name('groups.update');
Route::middleware('auth:sanctum')->delete('/groups/{id}', [GroupController::class, 'destroy'])
    ->name('groups.destroy');