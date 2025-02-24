using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvIcon : MonoBehaviour
{
    public int myIndex;
    InventoryShow inv;
    void Start()
    {
        inv = transform.parent.parent.GetComponent<InventoryShow>();
    }
    private void OnMouseDown()
    {
        inv.SelectItem(myIndex);
    }
}
