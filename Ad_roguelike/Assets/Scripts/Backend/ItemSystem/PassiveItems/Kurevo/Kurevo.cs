using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurevo : Item
{
    public override void Init()
    {
        switch (rarity)
        {
            case Rarity.common:
                character.PlusMults(0.05f, 0.05f);
                break;
            case Rarity.uncommon:
                character.PlusMults(0.12f, 0.12f);
                break;
            case Rarity.rare:
                character.PlusMults(0.2f, 0.2f);
                break;
            case Rarity.special:
                character.PlusMults(0.5f, 0.5f);
                character.CanRegenerateOnNewStage = false;
                break;
        }
    }

    public override void OnFightEnd()
    {
        switch (rarity)
        {
            case Rarity.common:
                break;
            case Rarity.uncommon:
                character.Heal(-6);
                break;
            case Rarity.rare:
                character.Heal(-character.MaxHealth * 0.1f);
                break;
            case Rarity.special:
                break;
        }
    }
}
