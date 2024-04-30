<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class NoteSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table("notes")->insert([
            [
                "title" => "Ókori civilizációk",
                "text" => "Egyiptomi Birodalom, Görög Poliszok, Római Köztársaság",
                "user_id" => 1,
                "topic_id" => 1,
                "updated_at" => now()->subDay(5),
                "role" => "Tanár"
            ],
            [
                "title" => "Középkori Európa",
                "text" => "A Karoling Királyság, A Keresztes Háborúk, A Fekete Halál",
                "user_id" => 1,
                "topic_id" => 1,
                "updated_at" => now()->subDay(3),
                "role" => "Tanár"
            ],
            [
                "title" => "Modern korszak",
                "text" => "Francia Forradalom, I. Világháború, II. Világháború",
                "user_id" => 1,
                "topic_id" => 1,
                "updated_at" => now()->subDay(10),
                "role" => "Tanár"
            ]
        ]);
    }
}
