using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Item
{
    float TotalMult = 0;
    public override void OnEachSec()
    {
        switch (rarity)
        {
            case Rarity.common:
                character.PlusMults(0.1f, 0.1f);
                TotalMult += 0.1f;
                break;
            case Rarity.uncommon:
                character.PlusMults(0.2f, 0.2f);
                TotalMult += 0.2f;
                break;
            case Rarity.rare:
                character.PlusMults(0.3f, 0.3f);
                TotalMult += 0.3f;
                break;
            case Rarity.special:
                character.PlusMults(0.6f, 0.6f);
                TotalMult += 0.6f;
                break;
        }
    }

    public override void OnActiveArtUsed(int index)
    {
        switch (rarity)
        {
            case Rarity.common:

                break;
            case Rarity.uncommon:
                character.Heal(-5);
                break;
            case Rarity.rare:
                character.Heal(-character.MaxHealth * 0.1f);
                break;
            case Rarity.special:
                if(Random(25))
                    character.DeleteActiveItem(index);
                break;
        }
    }

    public override void OnFightEnd()
    {
        character.PlusMults(-TotalMult, -TotalMult);
        TotalMult = 0;
    }
}
