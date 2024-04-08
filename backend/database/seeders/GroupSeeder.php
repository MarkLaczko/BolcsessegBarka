<?php

namespace Database\Seeders;

use App\Models\Group;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class GroupSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('groups')->insert([
            [
                'name' => '9.a'
            ],
            [
                'name' => '9.b'
            ],
            [
                'name' => '9.c'
            ],
            [
                'name' => '10.a'
            ],
            [
                'name' => '10.b'
            ],
            [
                'name' => '10.c'
            ],
            [
                'name' => '11.a'
            ],
            [
                'name' => '11.b'
            ],
            [
                'name' => '11.c'
            ],
            [
                'name' => '12.a'
            ],
            [
                'name' => '12.c'
            ],
            [
                'name' => '13.a'
            ],
            [
                'name' => '13.b'
            ],
            [
                'name' => '13.c'
            ],
        ]);
    }
}
