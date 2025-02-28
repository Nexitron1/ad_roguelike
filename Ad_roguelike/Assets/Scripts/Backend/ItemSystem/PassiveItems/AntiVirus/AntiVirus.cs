using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirus : Item
{
    //настоящий
    public override void Init()
    {
        character = Camera.main.GetComponent<Character>();
        float t = character.MaxHealth;
        switch (rarity)
        {
            case Rarity.common:              
                character.MaxHealth += 10;
                break;
            case Rarity.uncommon:
                character.MaxHealth += 20;
                character.PlusMults(-0.1f, -0.1f);
                break;
            case Rarity.rare:
                character.MaxHealth *= 1.1f;
                character.PlusMults(-0.25f, -0.25f);
                break;
            case Rarity.special:
                character.MaxHealth *= 1.5f;
                character.PlusMults(-0.6f, -0.6f);
                break;
        }
        character.Health += character.MaxHealth - t;
    }
}
