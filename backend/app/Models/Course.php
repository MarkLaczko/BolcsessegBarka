<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasManyThrough;

class Course extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name",
        "image",
        "created_by"
    ];

    public function groups(): BelongsToMany
    {
        return $this->belongsToMany(Group::class, "course_group");
    }

    public function users(): HasManyThrough
    {
        return $this->hasManyThrough(User::class, Group::class, "course_id", "user_id", "id", "group_id");
    }

    public function topics(): HasMany
    {
        return $this->hasMany(Topic::class);
    }

    public function creator(): BelongsTo
    {
        return $this->belongsTo(User::class, "created_by");
    }
}
