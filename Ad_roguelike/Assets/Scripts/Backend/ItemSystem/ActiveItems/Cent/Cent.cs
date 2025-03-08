using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cent : ActiveItem
{
    public override void Activate()
    {
        character.Heal(-1);
        character.endTime += 1;
        character.money += 1;
    }
}
