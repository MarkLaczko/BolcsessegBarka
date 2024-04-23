<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('assignment', function (Blueprint $table) {
            $table->id();
            $table->string("task_name");
            $table->string("comment")->nullable();
            $table->dateTime("deadline");
            $table->integer("grade")->nullable();
            $table->unsignedBigInteger("courseable_id");
            $table->string("courseable_type");
            $table->string("teacher_task_name")->nullable();
        });

        DB::statement("ALTER TABLE assignment ADD teacher_task LONGBLOB");
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('assignment');
    }
};
