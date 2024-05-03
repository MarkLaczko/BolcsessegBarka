<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class TopicSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('topics')->insert([
            [
                'name' => 'Történelmi korszakok',
                'course_id' => 1
            ],
            [
                'name' => 'Függvények',
                'course_id' => 2
            ]
        ]);
    }
}
