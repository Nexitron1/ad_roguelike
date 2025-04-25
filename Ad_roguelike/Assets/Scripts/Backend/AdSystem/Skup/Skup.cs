using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skup : AdFeature
{
    float MultLeft, MultRight;
    public override void Init()
    {
        base.Init();
        MultLeft = ch.MultLeft;
        MultRight = ch.MultRight;
        int n = ch.money;

        switch (myDiff)
        {
            case Difficulty.easy:
                ch.PlusMults((n / 10f) * -0.015f, (n / 10f) * -0.015f);
                break;
            case Difficulty.normal:
                ch.PlusMults((n / 10f) * -0.025f, (n / 10f) * -0.025f);
                break;
            case Difficulty.hard:
                ch.PlusMults((n / 9f) * -0.035f, (n / 9f) * -0.035f);
                break;
            case Difficulty.extreme:
                ch.PlusMults((n / 1f) * -0.001f, (n / 1f) * -0.001f);
                break;


        }
    }
    public override void OnFigthEnd()
    {
        base.OnFigthEnd();
        ch.MultRight = MultRight;
        ch.MultLeft = MultLeft;
    }
}