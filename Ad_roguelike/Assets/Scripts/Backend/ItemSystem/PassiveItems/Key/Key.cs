using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public override void Init()
    {
        switch (rarity) {
            case Rarity.common:
                character.generator.TreasureChance.Add(2);
                break;
            case Rarity.uncommon:
                character.generator.TreasureChance.Add(5);
                character.BaseAdLengh += 4;
                break;
            case Rarity.rare:
                character.generator.TreasureChance.Add(9);
                character.BaseAdLengh += 7;
                break;
            case Rarity.special:
                character.generator.TreasureChance.Add(12);
                break;
        }
    }
    public override void OnAdStart()
    {
        if(rarity == Rarity.special) 
            if (character.endTime < 25)
            {
                character.endTime = 25;
            }
    }
}
