using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScotch : ActiveItem
{
    public override void OnActivate()
    {
        character.Health += 5;
    }
}
