<?php

namespace App\Providers;

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
        Gate::define('users.get', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Csak adminisztrátor kérhet le felhasználókat!");
        });

        Gate::define('users.show', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Csak adminisztrátor kérhet le felhasználót!");
        });

        Gate::define('users.delete', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Csak adminisztrátor törölhet felhasználót!");
        });

        Gate::define('users.update', function (User $user) {
            return ($user->is_admin == 1)
                ? Response::allow()
                : Response::deny("Csak adminisztrátor frissítheti a felhasználókat!");
        });
    }
}
