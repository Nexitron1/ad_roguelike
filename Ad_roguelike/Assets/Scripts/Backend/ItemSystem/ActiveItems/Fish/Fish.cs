using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : ActiveItem
{
    public override void Activate()
    {
        character.AddStats(Stats.Statistics.FishUsed, 1);
        character.endTime += 5;
        character.PlusMults(0.25f, 0.25f);
    }
}
