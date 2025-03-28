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
                ch.DenyTime(2);
                break;
            case Difficulty.normal:
                ch.DenyTime(0.1f, true);
                break;
            case Difficulty.hard:
                ch.DenyTime(5);
                break;
            case Difficulty.extreme:
                ch.DenyTime(5);
                ch.DenyTime(0.15f, true);
                break;
        }
    }
}
