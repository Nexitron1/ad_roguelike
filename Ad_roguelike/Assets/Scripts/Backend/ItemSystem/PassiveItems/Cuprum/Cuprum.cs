using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuprum : Item
{


    //Всё что выше надо копипастить
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
                character.SkipTimeByDurationAndPlace(2f, "end");
                break;
            case Rarity.uncommon:
                character.SkipTimeByDurationAndPlace(5f, "start");
                break;
            case Rarity.rare:
                character.SkipTimeByPercentAndPlace(0.2f, "end");
                break;
            case Rarity.special:
                character.SkipTimeByPercentAndPlace(0.5f, "start");
                break;
        }
    }

    public override void OnEachFrame()
    {
        if (character == null)
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
                if (character.MaxHealth > 20)
                {
                    character.MaxHealth = 20;
                }
                if (character.Health > 20) { character.Health = 20; }
                break;
        }
    }
}
