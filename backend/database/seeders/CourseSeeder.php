<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class CourseSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('courses')->insert([
            [
                'name' => 'Történelem',
                'image' => 'tortenelem.png'
            ],
            [
                'name' => 'Matematika',
                'image' => 'matematika.png'
            ]
        ]);
        
        DB::table('course_group')->insert([
            [
                'course_id' => '1',
                'group_id' => '1'
            ]
        ]);
    }
}
