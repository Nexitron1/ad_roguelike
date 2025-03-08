using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSortOrder : SortOrder
{
    [SerializeField] GameObject kup;
    public override void MoveChilds(int index)
    {
        kup.GetComponent<SpriteRenderer>().sortingOrder = 5 + 100 * index;
    }
}
