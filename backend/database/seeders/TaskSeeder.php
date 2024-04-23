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
                'header' => 'A feladat a nagy földrajzi felfedezésekkel kapcsolatos',
                'text' => 'A források és ismeretei alapján válaszoljon a kérdésekre! Használja a középiskolai történelmi atlaszt!',
            ]
        ]);
    }
}
