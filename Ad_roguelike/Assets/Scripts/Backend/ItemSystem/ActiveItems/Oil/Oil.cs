using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : ActiveItem
{
    public override void Activate()
    {
        if (Random(80))
        {
            character.SetRoom(MapGenerator.RoomTypes.Treasure);
        }
        else
        {
            character.SetRoom(MapGenerator.RoomTypes.Fight);
        }
    }
}
