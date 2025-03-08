using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroIcons : MonoBehaviour
{
    public Shop shop;
    public int id;
    public bool Buyed = false;
    private void OnMouseDown()
    {
        if(!Buyed)
            shop.Select(id);
    }
}
