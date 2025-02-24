using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySortOrder : SortOrder
{
    public override void MoveChilds(int index)
    {
        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = 1 + 100 * index;
                transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2 + 100 * index;

            }
        }
        transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 1 + 100 * index;
        transform.GetChild(4).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder= 2 + 100 * index;
        transform.GetChild(5).GetComponent<SpriteRenderer>().sortingOrder = 1 + 100 * index;
        transform.GetChild(6).GetComponent<SpriteRenderer>().sortingOrder = 1 + 100 * index;
    }
}
