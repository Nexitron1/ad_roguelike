using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathetic : AdFeature
{
    float damageBoosted = 0, StartDamage = 0;

    public override void Init()
    {
        base.Init();
        StartDamage = ch.Damage;
        ch.Damage += damageBoosted;
    }

    public override void OnEachSec()
    {
        base.OnEachSec();
        switch (myDiff)
        {
            case Difficulty.easy:
                damageBoosted = 0.2f;
                ch.Damage += 0.2f;
                break;
            case Difficulty.normal:
                damageBoosted = 0.3f;
                ch.Damage += 0.3f;
                break;
            case Difficulty.hard:
                damageBoosted = 0.5f;
                ch.Damage += 0.5f;
                break;
            case Difficulty.extreme:
                damageBoosted = 0.8f;
                ch.Damage += 0.8f;
                break;
        }
    }
    public override void OnFigthEnd()
    {
        base.OnFigthEnd();
        ch.Damage = StartDamage;
    }
}