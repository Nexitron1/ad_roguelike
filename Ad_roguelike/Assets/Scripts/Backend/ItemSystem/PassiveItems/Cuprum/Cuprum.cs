using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuprum : Item
{
    public override void Init()
    {
        character = Camera.main.GetComponent<Character>();
        switch (rarity)
        {
            case Rarity.common:
                
                break;
            case Rarity.uncommon:
                character.MaxHealth -= 20;
                break;
            case Rarity.rare:
                character.MaxHealth = (int)(character.MaxHealth * 0.65f);
                break;
            case Rarity.special:
                if(character.MaxHealth > 20)
                {
                    character.MaxHealth = 20;
                }
                break;
        }
    }

    public override void OnAdStart()
    {
        if(character == null)
            character = Camera.main.GetComponent<Character>();

        switch (rarity) {
            case Rarity.common: 
                character.SkipTime(2f, TimeSkip.DurationType.absolute, -2f, TimeSkip.PlaceType.absolute);
                break;
            case Rarity.uncommon:
                character.SkipTime(5f, TimeSkip.DurationType.absolute, 0f, TimeSkip.PlaceType.absolute);
                break;
            case Rarity.rare:
                character.SkipTime(0.2f, TimeSkip.DurationType.percent, -0.2f, TimeSkip.PlaceType.percent);
                break;
            case Rarity.special:
                character.SkipTime(0.5f, TimeSkip.DurationType.percent, 0, TimeSkip.PlaceType.percent);
                break;
        }
    }

    public override void OnEachFrame()
    {
        if (character == null)
            character = Camera.main.GetComponent<Character>();

        if (rarity == Rarity.special) 
        { 
            if (character.MaxHealth > 20)
            {
                character.MaxHealth = 20;
            }
        }
    }
}
