using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poor : AdFeature
{
    public override void Init()
    {
        base.Init();

        int n = ch.items.Count;

        switch (myDiff)
        {
            case Difficulty.easy:
                ch.endTime += n * 0.3f;
                break;
            case Difficulty.normal:
                ch.endTime += n * 0.02f * ch.endTime;
                break;
            case Difficulty.hard:
                ch.endTime += n * 0.8f;
                break;
            case Difficulty.extreme:
                ch.endTime += n * 1f;
                ch.endTime += n * 0.02f * ch.endTime;
                break;


        }
    }
}
