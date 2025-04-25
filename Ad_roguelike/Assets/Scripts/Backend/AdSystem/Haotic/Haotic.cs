using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haotic : AdFeature
{
    float MultLeft, MultRight;
    public override void OnFigthEnd()
    {
        base.OnFigthEnd();
        ch.MultRight = MultRight;
        ch.MultLeft = MultLeft;
    }
    public override void Init()
    {
        base.Init();
        MultLeft = ch.MultLeft;
        MultRight = ch.MultRight;

        int n = ch.items.Count;

        switch (myDiff)
        {
            case Difficulty.easy:
                ch.PlusMults(n * -0.03f, n * -0.03f);
                break;
            case Difficulty.normal:
                ch.PlusMults(n * -0.05f, n * -0.05f);
                break;
            case Difficulty.hard:
                ch.PlusMults(n * -0.07f, n * -0.07f);
                break;
            case Difficulty.extreme:
                ch.PlusMults(n * -0.14f, n * -0.14f);
                break;


        }
    }
}