<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class SubtaskSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        DB::table('subtasks')->insert([
            [
                'task_id' => 1,
                'order' => 0,
                'question' => '<p>"Mindazok a szigetek, illetve szárazföldi területek, amelyeket Portugália királya, illetve az ő hajói felfedeztek, vagy fel fognak fedezni, ezen határvonaltól [a Zöld-foki-szigetektől] keletre, illetve azon belül északra vagy délre, az említett portugál király Úr és az ő utódai örökös tulajdonába mennek át. Azok pedig, amelyeket Kasztília és Aragon királya és királynője, illetve az ő hajójuk az említett határvonaltól nyugatra fedeztek fel, vagy fognak felfedezni északon vagy délen, az említett király Úr, illetve királynő és az ő utódai örökös tulajdonába mennek át." (A tordesillasi szerződés, 1494.június 7.)</p><br><p><b>a) A szerződés megkötését milyen nagy fontosságú történelmi esemény előzte meg 1492-ben?</b></p>',
                'options' => null,
                'solution' => '["Amerika felfedezése", "Kolumbusz Kristóf eljutott Amerikába"]',
                'type' => 'short_answer',
                'marks' => 0.5,
            ],
            [
                'task_id' => 1,
                'order' => 1,
                'question' => '<p><b>b) A tordesillasi szerződés alapján mely államot illették meg a következő területek? Jelölje X jellel a táblázat megfelelő helyén!</b></p><ul><li>Afrika</li></ul>',
                'options' => '["Spanyolország", "Portugália"]',
                'solution' => '["Portugália"]',
                'type' => 'multiple_choice',
                'marks' => 0.5,
            ],
            [
                'task_id' => 1,
                'order' => 2,
                'question' => '<ul><li>India</li></ul>',
                'options' => '["Spanyolország", "Portugália"]',
                'solution' => '["Portugália"]',
                'type' => 'multiple_choice',
                'marks' => 0.5,
            ],
            [
                'task_id' => 1,
                'order' => 3,
                'question' => '<ul><li>Közép-Amerika</li></ul>',
                'options' => '["Spanyolország", "Portugália"]',
                'solution' => '["Spanyolország"]',
                'type' => 'multiple_choice',
                'marks' => 0.5,
            ],
            [
                'task_id' => 1,
                'order' => 4,
                'question' => '<p><b>Ki volt az a felfedező, aki elsőként jutott el Indiába Afrika megkerülésével? Melyik királyság szolgálatában állt?</b></p>',
                'options' => null,
                'solution' => '["Vasco de Gama, Portugália"]',
                'type' => 'short_answer',
                'marks' => 1,
            ],
            [
                'task_id' => 1,
                'order' => 5,
                'question' => '<p><b>d) Mi volt ennek a felfedezőútnak a jelentősége?</b></p>',
                'options' => null,
                'solution' => '["fűszerkereskedelem (bors) monopóliumának megszerzése, vagy az összes közvetítő kereskedelem kiküszöbölését tette lehetővé."]',
                'type' => 'short_answer',
                'marks' => 1,
            ],
        ]);
    }
}
