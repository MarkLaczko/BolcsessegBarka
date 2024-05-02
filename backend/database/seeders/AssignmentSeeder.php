<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

use function Laravel\Prompts\table;

class AssignmentSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('assignment')->insert([
            [
                'task_name' => 'Ókori civilizációk kutatása',
                'comment' => 'Az ókori Egyiptom, Görögország és Róma társadalmi szerkezete és kultúrája.',
                'deadline' => now()->addWeeks(2),
                'courseable_id' => 1,
                'topic_id' => 1,
            ],
            [
                'task_name' => 'Középkori Európa elemzése',
                'comment' => 'Elemzés a középkori Európa politikai és gazdasági viszonyairól.',
                'deadline' => now()->addWeeks(3),
                'courseable_id' => 1,
                'topic_id' => 1,
            ],
            [
                'task_name' => 'Modern korszak fordulópontjai',
                'comment' => 'A jelentős történelmi fordulópontok hatása a modern társadalomra.',
                'deadline' => now()->addWeeks(4),
                'courseable_id' => 1,
                'topic_id' => 1,
            ]
        ]);
    }
}
