using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Item
{
    public override void OnAdStart()
    {
        switch (rarity)
        {
            case Rarity.common:
                character.SkipTime(0.005f * character.items.Count, TimeSkip.DurationType.percent, 0, TimeSkip.PlaceType.absolute);
                break;
            case Rarity.uncommon:
                character.SkipTime(0.01f * character.items.Count, TimeSkip.DurationType.percent, 0, TimeSkip.PlaceType.absolute);
                character.Heal(-1 * character.items.Count);
                break;
            case Rarity.rare:
                Debug.LogWarning("Безграмотный Иван");
                character.Heal(-7 * character.items.Count);
                break;
            case Rarity.special:
                character.endTime -= 15;
                //когда удаление
                break;
        }
    }
}
