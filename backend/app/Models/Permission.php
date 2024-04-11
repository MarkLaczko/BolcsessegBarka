<?php

namespace App\Models;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Permission extends Model
{
    public $timestamps = false;

    protected $fillable = [
        "name"
    ];

    public function users() : BelongsToMany
    {
        return $this->belongsToMany(User::class, "member")->as('member')->withPivot('group_id');
    }

    public function groups() : BelongsToMany
    {
        return $this->belongsToMany(Group::class, "member")->as('member')->withPivot('user_id');
    }
}
