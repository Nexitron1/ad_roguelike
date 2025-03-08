using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterBox : Item
{
    public override void Init()
    {
        switch (rarity) 
        { 
            case Rarity.common:
                character.MoneyMultiplier += 0.05f;
                break;
            case Rarity.uncommon:
                character.MoneyAdder += 5;
                character.BossTimeAdder += 10;
                break;
            case Rarity.rare:
                character.MoneyMultiplier += 0.15f;
                character.BossTimeAdder += 20;
                break;
            case Rarity.special:
                character.MoneyAdder += 15;
                break;
        }
    }
    public override void OnAdStart()
    {
        if(rarity == Rarity.special)
        {
            if (character.GetRoomType() == MapGenerator.RoomTypes.Boss)
            {
                character.CanUseActiveArts = false;
                stage = character.stage;
            }
        }
    }
    int stage = 1;
    public override void OnFightEnd()
    {
        if (rarity == Rarity.special)
        {
            if (stage < character.stage)
            {
                character.CanUseActiveArts = true;
                stage = character.stage;
            }

        }
    }
}
