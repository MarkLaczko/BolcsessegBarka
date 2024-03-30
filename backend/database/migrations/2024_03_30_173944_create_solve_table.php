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
        Schema::create('solve', function (Blueprint $table) {
            $table->foreignId("task_id")->constrained();
            $table->foreignId("quiz_id")->constrained();
            $table->primary(["task_id","quiz_id"]);
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('solve');
    }
};
