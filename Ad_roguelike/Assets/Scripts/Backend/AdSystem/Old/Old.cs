using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Old : AdFeature
{
    public override void OnEachSec()
    {
        base.OnEachSec();

        switch (myDiff)
        {
            case Difficulty.easy:
                if (!Random(0.98f))
                {
                    ch.OldAdFeature();
                }
                break;
            case Difficulty.normal:
                if (!Random(0.99f))
                {
                    ch.OldAdFeature();
                    ch.OldAdFeature();
                }
                break;
            case Difficulty.hard:
                if (!Random(0.96f))
                {
                    ch.OldAdFeature();
                }
                break;
            case Difficulty.extreme:
                if (!Random(0.995f))
                {
                    ch.OldAdFeature();
                    ch.OldAdFeature();
                    ch.OldAdFeature();
                    ch.OldAdFeature();
                    ch.OldAdFeature();
                }
                break;


        }
    }
}