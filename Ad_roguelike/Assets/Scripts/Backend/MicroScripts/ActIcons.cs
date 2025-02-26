using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActIcons : MonoBehaviour
{
    Hotbar hb;
    public int id;
    private void Start()
    {
        hb = transform.parent.parent.parent.parent.GetComponent<Hotbar>();
    }

    private void OnMouseDown()
    {
        hb.ActivateItem(id);
    }
}
