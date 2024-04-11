<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Group extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name"
    ];
    
    public function users() : BelongsToMany
    {
        return $this->belongsToMany(User::class, "member")->as('member')->withPivot('permission_id');
    }

    public function permissions() : BelongsToMany
    {
        return $this->belongsToMany(Permission::class, "member")->as('member')->withPivot('user_id');
    }

    public function courses() : BelongsToMany
    {
        return $this->belongsToMany(Course::class, "course_group");
    }

}
