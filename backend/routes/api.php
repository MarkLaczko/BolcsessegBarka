<?php

use App\Http\Controllers\AssignmentController;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\CourseController;
use App\Http\Controllers\GroupController;
use App\Http\Controllers\NoteController;
use App\Http\Controllers\PermissionController;
use App\Http\Controllers\TopicController;
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

Route::middleware('auth:sanctum')->get('/users', [UserController::class, 'index'])
    ->name('users.index');
Route::middleware('auth:sanctum')->get('/users/{id}', [UserController::class, 'show'])
    ->name('users.show');
Route::middleware('auth:sanctum')->post('/users/delete', [UserController::class, 'bulkDelete'])
    ->name('users.bulkDelete');
Route::middleware('auth:sanctum')->put('/users/{id}', [UserController::class, 'update'])
    ->name('users.update');
Route::middleware('auth:sanctum')->delete('/users/{id}', [UserController::class, 'destroy'])
    ->name('users.destroy');


Route::post('/users/register', [UserController::class, 'store'])
    ->name('users.store');
Route::post('/users/login', [AuthController::class, 'authenticate'])
    ->name('users.login');
Route::middleware('auth:sanctum')->put('/user/profile', [UserController::class, 'updateProfile'])
    ->name('profie.update');

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
Route::middleware('auth:sanctum')->post('/courses/delete', [CourseController::class, 'bulkDelete'])
    ->name('courses.bulkDelete');
Route::middleware('auth:sanctum')->post('/courses/{course}/groups', [CourseController::class, 'assignGroups'])
    ->name('courses.assignGroups');
Route::middleware('auth:sanctum')->post('/courses/{course}/topics', [CourseController::class, 'assignTopics'])
    ->name('courses.assignTopics');
Route::middleware('auth:sanctum')->get('/courses/{id}/with-users', [CourseController::class, 'showCourseWithUsers'])
    ->name('courses.showCourseWithUsers');

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
Route::middleware('auth:sanctum')->delete('/groups', [GroupController::class, 'bulkDelete'])
    ->name('groups.bulkDelete');


Route::middleware('auth:sanctum')->get('/notes', [NoteController::class,'index'])
    ->name('notes.index'); 
Route::middleware('auth:sanctum')->get('/notes/{id}', [NoteController::class,'show'])
    ->name('notes.show'); 
Route::middleware('auth:sanctum')->post('/notes', [NoteController::class,'store'])
    ->name('notes.store');
Route::middleware('auth:sanctum')->put('/notes/{id}', [NoteController::class,'update'])
    ->name('notes.update');
Route::middleware('auth:sanctum')->delete('/notes/{id}', [NoteController::class,'destroy'])
    ->name('notes.destroy');

Route::middleware('auth:sanctum')->get('/assignments', [AssignmentController::class,'index'])
    ->name('assignments.index'); 
Route::middleware('auth:sanctum')->get('/assignments/{id}', [AssignmentController::class,'show'])
    ->name('assignments.show'); 
Route::middleware('auth:sanctum')->post('/assignments', [AssignmentController::class,'store'])
    ->name('assignments.store');
Route::middleware('auth:sanctum')->put('/assignments/{id}', [AssignmentController::class,'update'])
    ->name('assignments.update');
Route::middleware('auth:sanctum')->delete('/assignments/{id}', [AssignmentController::class,'destroy'])
    ->name('assignments.destroy');

Route::middleware('auth:sanctum')->get('/topics', [TopicController::class,'index'])
    ->name('topics.index'); 
Route::middleware('auth:sanctum')->get('/topics/{id}', [TopicController::class,'show'])
    ->name('topics.show'); 
Route::middleware('auth:sanctum')->post('/topics', [TopicController::class,'store'])
    ->name('topics.store');
Route::middleware('auth:sanctum')->put('/topics/{id}', [TopicController::class,'update'])
    ->name('topics.update');
Route::middleware('auth:sanctum')->delete('/topics/{id}', [TopicController::class,'destroy'])
    ->name('topics.destroy');
