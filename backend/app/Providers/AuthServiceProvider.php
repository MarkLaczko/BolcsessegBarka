<?php

namespace App\Providers;

use App\Models\Answer;
use App\Models\Attempt;
use App\Models\Course;
use App\Models\Note;
use App\Models\Quiz;
use App\Models\Subtask;
use App\Models\Task;
use App\Models\User;
use Illuminate\Support\Facades\Gate;
use Illuminate\Auth\Access\Response;
use Illuminate\Foundation\Support\Providers\AuthServiceProvider as ServiceProvider;

class AuthServiceProvider extends ServiceProvider
{
    /**
     * The model to policy mappings for the application.
     *
     * @var array<class-string, class-string>
     */
    protected $policies = [
        //
    ];

    /**
     * Register any authentication / authorization services.
     */
    public function boot(): void
    {
        //User gates
        Gate::define('users.get', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can get users!");
        });

        Gate::define('users.show', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can get a user!");
        });

        Gate::define('users.delete', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can delete users!");
        });

        Gate::define('users.update', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can update users!");
        });

        //Group gates
        Gate::define('groups.store', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can store groups!");
        });

        Gate::define('groups.update', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can update groups!");
        });

        Gate::define('groups.delete', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can delete groups!");
        });

        //Course gates

        Gate::define('courses.store', function (User $user) {
            if ($user->is_admin == 1) {
                return Response::allow();
            } else {
                $isTeacher = $user->groups()->wherePivot("permission", "Tanár")->exists();
                return $isTeacher
                    ? Response::allow()
                    : Response::deny("You must be an administrator or a teacher to create a course!");
            }
        });

        Gate::define('courses.update', function (User $user, Course $course) {
            return ($user->is_admin == 1 || $user->id == $course->created_by)
                ? Response::allow()
                : Response::deny("You must be an administrator or the creator of this course to update it!");
        });

        Gate::define('courses.destroy', function (User $user, Course $course) {
            return ($user->is_admin == 1 || $user->id == $course->created_by)
                ? Response::allow()
                : Response::deny("You must be an administrator or the creator of this course to delete it!");
        });

        Gate::define('courses.assignGroups', function (User $user) {
            if ($user->is_admin == 1) {
                return Response::allow();
            } else {
                $isTeacher = $user->groups()->wherePivot("permission", "Tanár")->exists();
                return $isTeacher
                    ? Response::allow()
                    : Response::deny("You must be an administrator or a teacher to assign a group to a course!");
            }
        });

        Gate::define('courses.assignTopics', function (User $user) {
            if ($user->is_admin == 1) {
                return Response::allow();
            } else {
                $isTeacher = $user->groups()->wherePivot("permission", "Tanár")->exists();
                return $isTeacher
                    ? Response::allow()
                    : Response::deny("You must be an administrator or a teacher to assign a topic to a course!");
            }
        });

        //Topic gates

        Gate::define('topics.store', function (User $user) {
            if ($user->is_admin == 1) {
                return Response::allow();
            } else {
                $isTeacher = $user->groups()->wherePivot("permission", "Tanár")->exists();
                return $isTeacher
                    ? Response::allow()
                    : Response::deny("You must be an administrator or a teacher to create a topic!");
            }
        });

        Gate::define('topics.update', function (User $user) {
            if ($user->is_admin == 1) {
                return Response::allow();
            } else {
                $isTeacher = $user->groups()->wherePivot("permission", "Tanár")->exists();
                return $isTeacher
                    ? Response::allow()
                    : Response::deny("You must be an administrator or a teacher to update a topic!");
            }
        });

        Gate::define('topics.destroy', function (User $user) {
            if ($user->is_admin == 1) {
                return Response::allow();
            } else {
                $isTeacher = $user->groups()->wherePivot("permission", "Tanár")->exists();
                return $isTeacher
                    ? Response::allow()
                    : Response::deny("You must be an administrator or a teacher to delete a topic!");
            }
        });

        //Note gates

        Gate::define('notes.update', function (User $user, Note $note) {
            return ($user->id == $note->user_id)
                ? Response::allow()
                : Response::deny("You must be the creator of this note to update it!");
        });

        Gate::define('notes.destroy', function (User $user, Note $note) {
            return ($user->id == $note->user_id)
                ? Response::allow()
                : Response::deny("You must be the creator of this note to delete it!");
        });

        //Quiz gates
        Gate::define('quizzes.getAll', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Only administrators can get all quizzes!");
        });

        Gate::define('quizzes.get', function (User $user, Quiz $quiz) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($quiz->topic->course->groups as $value) {
                if(in_array($user->id, array_map(fn($x) => $x['id'], $value->users->toArray()))){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course to view it!");
        });

        Gate::define('quizzes.store', function (User $user, Course $course) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this course to create quizzes in it!");
        });

        Gate::define('quizzes.update', function (User $user, Course $course) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course to update it!");
        });

        Gate::define('quizzes.destroy', function (User $user, Quiz $quiz) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }
            return Response::deny("You must have access to this quiz's course to delete it!");
        });

        Gate::define('quiz.tasks.get', function (User $user, Quiz $quiz) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            foreach ($quiz->attempts as $value) {
                if($value->user->toArray()['id'] == $user->id && $value->end == null){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('tasks.get', function (User $user, Task $task) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($task->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            foreach ($task->quiz->attempts as $value) {
                if($value->user->toArray()['id'] == $user->id && $value->end == null){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this tasks's course or have an ongoing attempt to get it!");
        });

        Gate::define('tasks.get.solutions', function (User $user, Task $task) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($task->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this tasks's course to get it's solutions!");
        });

        Gate::define('tasks.store', function (User $user, Quiz $quiz) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this course to create tasks in it!");
        });

        Gate::define('tasks.update', function (User $user, Task $task) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($task->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this tasks's course to update it!");
        });

        Gate::define('tasks.delete', function (User $user, Task $task) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($task->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this tasks's course to delete it!");
        });

        Gate::define('subtasks.solutions', function (User $user, Subtask $subtask) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($subtask->task->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this solution's course to get it's solutions!");
        });

        Gate::define('subtasks.delete', function (User $user, Subtask $subtask) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($subtask->task->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this subtasks's course to delete it!");
        });

        Gate::define('attempts.index', function (User $user) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('attempts.show', function (User $user, Attempt $attempt) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            foreach ($attempt->quiz->attempts as $value) {
                if($value->user->toArray()['id'] == $user->id && $value->end == null){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('attempts.quizAttempts', function (User $user, Quiz $quiz) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('attempts.store', function (User $user, Quiz $quiz) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($quiz->topic->course->groups as $value) {
                $memberIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id;
                });
                $attemptsByUser = array_filter($quiz->attempts->toArray(), function($x) use ($user) {
                    return $x['user_id'] == $user->id;
                });
                if(count($memberIn) > 0 && (isset($quiz->max_attempts) ? count($attemptsByUser) < $quiz->max_attempts : true)){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('attempts.update', function (User $user, Attempt $attempt) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('attempts.destroy', function (User $user, Attempt $attempt) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('attempts.finish', function (User $user, Attempt $attempt) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            foreach ($attempt->quiz->attempts as $value) {
                if($value->user->toArray()['id'] == $user->id && $value->end == null){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('answers.index', function (User $user) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('answers.store', function (User $user, Attempt $attempt) {
            if($attempt->end == null && ($attempt->user->id == $user->id || $user->is_admin == 1)){
                return Response::allow();
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('answers.show', function (User $user, Answer $answer) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($answer->attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('answers.update', function (User $user, Answer $answer) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($answer->attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });

        Gate::define('answers.destroy', function (User $user, Answer $answer) {
            if($user->is_admin == 1){
                return Response::allow();
            }

            foreach ($answer->attempt->quiz->topic->course->groups as $value) {
                $teacerIn = array_filter($value->users->toArray(), function($x) use ($user) {
                    return $x['id'] == $user->id && $x['member']['permission'] == 'Tanár';
                });
                if(count($teacerIn) > 0){
                    return Response::allow();
                }
            }

            return Response::deny("You must have access to this quiz's course or have an ongoing attempt to get it!");
        });
    }
}
