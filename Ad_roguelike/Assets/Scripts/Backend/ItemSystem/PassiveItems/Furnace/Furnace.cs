using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Item
{
    int itemsBuyed;
    public override void Init()
    {
        switch (rarity)
        {
            case Rarity.common:

                break;
            case Rarity.uncommon:
                character.ShopAdder += 5;
                break;
            case Rarity.rare:
                character.ShopMultiplier = 1.1f;
                break;
            case Rarity.special:
                itemsBuyed = (int)character.GetStats(Stats.Statistics.ItemsBuyed);
                //когда статистику сделаю
                break;
        }
    }
    public override void OnEachFrame()
    {
        if (rarity == Rarity.special) 
        { 
            if(itemsBuyed < (int)character.GetStats(Stats.Statistics.ItemsBuyed))
            {
                character.CanBuyItems = false;
            }
        }
    }
    public override void OnNewStage()
    {
        if (rarity == Rarity.special)
        {
             character.CanBuyItems = true;
        }
    }
    public override void OnFightEnd()
    {
        switch (rarity)
        {
            case Rarity.common:
                character.Heal(4);
                break;
            case Rarity.uncommon:
                character.Heal(7);
                break;
            case Rarity.rare:
                character.Heal(character.MaxHealth * 0.07f);
                break;
            case Rarity.special:
                character.Heal(character.MaxHealth * 0.2f);
                break;
        }
    }
}
