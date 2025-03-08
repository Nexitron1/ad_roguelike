using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : ActiveItem
{
    public override void Activate()
    {
        character.DeleteItem(character.items.Count - 1);
        character.AddItem(character.GetComponent<ItemsHolder>().CreateItem());
    }
}

