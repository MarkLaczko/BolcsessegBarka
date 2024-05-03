<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class QuizSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('quizzes')->insert([
            [
                'id' => 1,
                'name' => '2023 május emelt',
                'topic_id' => 1,
                'max_attempts' => 5,
                'opens' => now()->subDay(),
                'closes' => now()->addDays(35),
                'time' => 60
            ],
            [
                'id' => 2,
                'name' => 'Röpdolgozat',
                'topic_id' => 2,
                'max_attempts' => 1,
                'opens' => now()->subDays(2),
                'closes' => now()->addDays(33),
                'time' => 10
            ]
        ]);
    }
}
