using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medali : Item
{
    public override void Init()
    {
        character = Camera.main.GetComponent<Character>();
        switch (rarity)
        {
            case Rarity.common:

                break;
            case Rarity.uncommon:

                break;
            case Rarity.rare:

                break;
            case Rarity.special:

                break;
        }
    }

    public override void OnActiveArtUsed(int index)
    {
        switch (rarity)
        {
            case Rarity.common:
                character.SkipTime(0.5f, TimeSkip.DurationType.absolute, 0, TimeSkip.PlaceType.absolute, false);
                break;
            case Rarity.uncommon:
                character.SkipTime(1f, TimeSkip.DurationType.absolute, 0, TimeSkip.PlaceType.absolute, false);
                break;
            case Rarity.rare:
                character.SkipTime(2f, TimeSkip.DurationType.absolute, 0, TimeSkip.PlaceType.absolute, false);
                break;
            case Rarity.special:
                if (Random(10))
                {
                    character.SkipAd();
                }
                break;
        }
    }

    public override void OnEachSec()
    {
        switch (rarity)
        {
            case Rarity.common:
                break;
            case Rarity.uncommon:
                character.Damage += 0.2f;
                break;
            case Rarity.rare:
                character.Damage += 0.4f;
                break;
            case Rarity.special:
                character.Damage += 1f;
                break;
        }
    }
}
