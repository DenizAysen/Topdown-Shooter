using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonVariables 
{
    public enum SpawnedObjects
    {
        Bullet,
        Barrier,
        Coin,
        CollectedParticle
    }
    public enum PanelTypes
    {
        Start = 0,
        Failed = 1,
        Success = 2,
        InGame = 3
    }
    public enum PlayerAnimsTriggers
    {
        Hit,
    }
    public enum PlayerAnimBools
    {
        Run,
        Shooting,
        Die
    }
    public enum CollectableType
    {
        GunUpgrade,
        Health,
    }
    public enum PlayerAnimState
    {
        Idle
    }
}
