using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trevogn : AdFeature
{
    public override void OnActiveArtUsed(int index)
    {
        base.OnActiveArtUsed(index);

        
        switch (myDiff)
        {
            case Difficulty.easy:
                if (!Random(0.95f)) 
                    ch.DeleteActiveItem(index);
                break;
            case Difficulty.normal:
                if (!Random(0.93f))
                    ch.DeleteItem(UnityEngine.Random.Range(0, ch.items.Count));
                break;
            case Difficulty.hard:
                if (!Random(0.88f))
                    ch.DeleteActiveItem(index);
                break;
            case Difficulty.extreme:
                if (!Random(0.93f))
                    ch.DeleteItem(UnityEngine.Random.Range(0, ch.items.Count));
                if (!Random(0.93f))
                    ch.DeleteActiveItem(index);
                break;


        }
    }
}