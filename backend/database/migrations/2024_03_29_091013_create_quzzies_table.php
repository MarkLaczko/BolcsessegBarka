<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('quzzies', function (Blueprint $table) {
            $table->id();
            $table->string("name");
            $table->string("attemps");
            $table->time("opens");
            $table->time("closes");
            $table->time("time");
            $table->integer("grade");
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('quzzies');
    }
};
