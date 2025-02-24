using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : Item
{

    //Всё что выше надо копипастить

    public override void Init()
    {
        character = Camera.main.GetComponent<Character>();
        switch (rarity)
        {
            case Rarity.common:               
                character.PlusMults(0.1f, 0);
                break;
            case Rarity.uncommon:
                character.PlusMults(0.25f, -0.25f);
                break;
            case Rarity.rare:
                character.PlusMults(0.5f, -0.4f);
                break;
            case Rarity.special:
                character.PlusMults(-1f, 1f);
                break;
        }
    }
}
