using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezlika : AdFeature
{
    public override void OnActiveArtUsed(int index)
    {
        switch (myDiff)
        {
            case Difficulty.easy:
                ch.endTime += 2;
                break;
            case Difficulty.normal:
                ch.endTime *= 1.1f;
                break;
            case Difficulty.hard:
                ch.endTime += 5;
                break;
            case Difficulty.extreme:
                ch.endTime += 5;
                ch.endTime *= 1.15f;
                break;
        }
    }
}
