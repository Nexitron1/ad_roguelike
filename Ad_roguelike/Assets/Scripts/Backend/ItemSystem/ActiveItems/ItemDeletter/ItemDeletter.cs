using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeletter : ActiveItem
{
    public override void Activate()
    {
        character.DeleteItem(UnityEngine.Random.Range(0, character.items.Count));
    }
}
