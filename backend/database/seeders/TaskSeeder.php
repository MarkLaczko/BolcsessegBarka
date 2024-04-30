<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class TaskSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('tasks')->insert([
            [
                'quiz_id' => 1,
                'order' => 0,
                'header' => 'A feladat a földrajzi felfedezések következményeihez kapcsolódik.',
                'text' => 'Oldja meg a feladatot a források és ismeretei segítségével!',
            ],
            [
                'quiz_id' => 1,
                'order' => 1,
                'header' => 'A feladat Németország 19-20. századi történetével kapcsolatos.',
                'text' => '(komplex korszakokon átívelő)',
            ]
        ]);
    }
}
