<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;

use App\Models\User;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        User::factory()->create([
            'name' => 'admin',
            'password' => 'admin123',
            'email' => "admin@admin.com",
            'is_admin' => 1
        ]);

        User::factory()->create([
            'name' => 'user',
            'password' => 'user123',
            'email' => "user@user.com",
            'is_admin' => 0
        ]);
    }
}
