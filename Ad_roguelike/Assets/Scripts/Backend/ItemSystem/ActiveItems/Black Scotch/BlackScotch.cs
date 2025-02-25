using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScotch : ActiveItem
{
    public override void OnActivate()
    {
        character.Health *= 1.05f;
    }
}
