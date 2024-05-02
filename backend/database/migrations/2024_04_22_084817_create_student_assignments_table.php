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
        Schema::create('student_assignments', function (Blueprint $table) {
            $table->id();
            $table->foreignId("assignment_id")->constrained("assignment","id")->onDelete('cascade');
            $table->string("student_task_name");
            $table->foreignId("user_id")->constrained("users","id")->onDelete('cascade');
        });
        DB::statement("ALTER TABLE student_assignments ADD student_task LONGBLOB");
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('student_assignments');
    }
};
