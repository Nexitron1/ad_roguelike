using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScotch : ActiveItem
{
    public override void Activate()
    {
        character.Heal(5);
    }
}
