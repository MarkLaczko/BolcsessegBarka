<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class StudentAssignmentSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('student_assignments')->insert([
            [
                "assignment_id" => 1,
                "student_task_name" => "Proba diák feladat",
                "student_task" => "proba",
                "user_id" => 1,
            ],
        ]);
    }
}
