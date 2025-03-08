using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : ActiveItem
{

    public override void Activate()
    {
        character.Heal(-character.MaxHealth * 0.05f * TotalTimesUsed);
        character.SkipTime(0.05f, TimeSkip.DurationType.percent, 0, TimeSkip.PlaceType.absolute, false);
    }
}
