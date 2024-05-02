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
            ],
            [
                "title" => "Másodfokú egyenlet",
                "text" => "x<sup>2</sup> + bx + c = 0<br>D = b<sup>2</sup> - 4 * a * c<br>x1,2 = (-b +- √D) / 2 * a",
                "user_id" => 1,
                "topic_id" => 2,
                "updated_at" => now()->subDay(3),
                "role" => "Tanár"
            ],
            [
                "title" => "Másodfokú egyenlet (órai jegyzetem)",
                "text" => "Nem értettem semmit :(",
                "user_id" => 2,
                "topic_id" => 2,
                "updated_at" => now()->subDay(3),
                "role" => "Tanuló"
            ],
        ]);
    }
}
