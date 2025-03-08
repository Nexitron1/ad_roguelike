using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScotch : ActiveItem
{
    public override void Activate()
    {
        character.Heal(character.MaxHealth * 0.1f);
    }
}
