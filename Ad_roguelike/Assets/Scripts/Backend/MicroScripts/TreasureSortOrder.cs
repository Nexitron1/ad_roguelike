using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSortOrder : SortOrder
{
    public override void MoveChilds(int index)
    {
        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5 + 100 * index;
        transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4 + 100 * index;
        transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 2 + 100 * index;
        transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 3 + 100 * index;
    }
}
