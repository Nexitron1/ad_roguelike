using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arctic : Item
{
    public override void OnAdStart()
    {
        switch (rarity)
        {
            case Rarity.common:
                if (Random(20))
                {
                    character.SkipTime(1f, TimeSkip.DurationType.absolute, UnityEngine.Random.Range(0, character.endTime), TimeSkip.PlaceType.absolute);
                    Debug.Log("20%");
                }
                

                break;
            case Rarity.uncommon:
                if (Random(15))
                {
                    character.SkipTime(2f, TimeSkip.DurationType.absolute, UnityEngine.Random.Range(0, character.endTime), TimeSkip.PlaceType.absolute);
                    Debug.Log("15%");
                }
                break;
            case Rarity.rare:
                if (Random(10))
                {
                    character.SkipTime(5f, TimeSkip.DurationType.absolute, UnityEngine.Random.Range(0, character.endTime), TimeSkip.PlaceType.absolute);
                    Debug.Log("10%");
                }
                break;
            case Rarity.special:
                if (Random(5))
                {
                    character.SkipAd();
                    Debug.Log("5%");
                }
                if (Random(1))
                {
                    character.Heal(0);
                }

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
                if (Random(3))
                {
                    character.DeleteItem(UnityEngine.Random.Range(0, character.items.Count));
                }
                break;
            case Rarity.rare:
                if (Random(7))
                {
                    character.DeleteItem(UnityEngine.Random.Range(0, character.items.Count));
                }
                break;
            case Rarity.special:

                break;
        }
    }
}
