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
            'password' => 'user1234',
            'email' => "user@user.com",
            'is_admin' => 0
        ]);

        $this->call([
            GroupSeeder::class,
            CourseSeeder::class,
            TopicSeeder::class,
            QuizSeeder::class,
            TaskSeeder::class,
            SubtaskSeeder::class,
            NoteSeeder::class,
            AssignmentSeeder::class,
            StudentAssignmentSeeder::class
        ]);
    }
}
