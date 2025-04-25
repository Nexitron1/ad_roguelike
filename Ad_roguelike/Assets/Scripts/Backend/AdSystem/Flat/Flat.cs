using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flat : AdFeature
{
    float MultLeft, MultRight;
    public override void Init()
    {
        base.Init();
        MultLeft = ch.MultLeft;
        MultRight = ch.MultRight;
    }

    public override void OnFigthEnd()
    {
        base.OnFigthEnd();
        ch.MultRight = MultRight;
        ch.MultLeft = MultLeft;
    }
    public override void OnEachSec()
    {
        base.OnEachSec();
        switch (myDiff)
        {
            case Difficulty.easy:
                ch.PlusMults(-0.01f, -0.01f);
                break;
            case Difficulty.normal:
                ch.PlusMults(-0.02f, -0.02f);
                break;
            case Difficulty.hard:
                ch.PlusMults(-0.04f, -0.04f);
                break;
            case Difficulty.extreme:
                ch.PlusMults(-0.07f, -0.07f);
                break;


        }
        
    }

}
