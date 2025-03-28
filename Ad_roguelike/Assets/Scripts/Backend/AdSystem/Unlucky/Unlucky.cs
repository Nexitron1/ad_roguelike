using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlucky : AdFeature
{
    float secsAdded = 0;

    public override void Init()
    {
        base.Init();
        ch.endTime += secsAdded;
    }

    public override void OnEachSec()
    {
        base.OnEachSec();
        switch (myDiff) 
        {
            case Difficulty.easy:
                if (!Random(0.99f))
                {
                    secsAdded += 1;
                    ch.endTime += 1;
                }
                break;
            case Difficulty.normal:
                if (!Random(0.99f))
                {
                    secsAdded += ch.endTime * 0.03f;
                    ch.endTime += ch.endTime * 0.03f;
                }
                break;
            case Difficulty.hard:
                if (!Random(0.985f))
                {
                    secsAdded += 2;
                    ch.endTime += 2;
                }
                break;
            case Difficulty.extreme:
                if (!Random(0.995f))
                {
                    ch.time = 0;
                }
                break;
        }
    }
}