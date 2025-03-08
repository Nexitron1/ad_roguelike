using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryShow : MonoBehaviour
{
    //[SerializeField] Color[] rarityColors;
    [SerializeField] TMP_Text Name, Good, Bad;
    [SerializeField] Transform LargeIcon;
    [SerializeField] Color defaultColor;
    Transform[] slots = new Transform[18];
    public Item[] items = new Item[18];
    public int itemsCount = 0;
    int Selected = -1;
    Character character;
    ItemsHolder ih;

    private void Start()
    {
        character = Camera.main.GetComponent<Character>();
        ih = Camera.main.GetComponent<ItemsHolder>();
        FillSlots();
        character.InventorySet(this);
        SelectItem(0);
    }
    public void RefreshIcons()
    {
        foreach (Transform sl in slots)
        {
            sl.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            sl.GetComponent<SpriteRenderer>().color = defaultColor;
        }
        LargeIcon.GetComponent<SpriteRenderer>().color = defaultColor;
        LargeIcon.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        Name.text = "";
        Good.text = "";
        Bad.text = "";
    }
    void FillSlots()
    {
        int i = 0;
        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 6; k++)
            {
                slots[i] = transform.GetChild(j + 1).GetChild(k);
                slots[i].GetComponent<InvIcon>().myIndex = i;
                i += 1;
            }
        }
    }
    public void AddItem(Item item)
    {
        if (itemsCount < 18)
        {


            items[itemsCount] = item;
            slots[itemsCount].GetChild(0).GetComponent<SpriteRenderer>().sprite = item.icon;
            slots[itemsCount].GetComponent<SpriteRenderer>().color = ih.rarityColors[(int)item.rarity];


            itemsCount++;
        }
        else
        {
            Debug.LogError("Reached limit of items");
        }
    }
    public void SelectItem(int select)
    {
        Selected = select;

        if (items[Selected] != null) 
        {
            var it = items[Selected];
            Name.text = it.itemName;
            string txt = it.descriptions[(int)it.rarity];
            var t = txt.Split('&');
            Good.text = t[0];
            if (t.Length > 1)
                Bad.text = t[1];
            else 
                Bad.text = "нет отрицательных свойств";

            LargeIcon.GetComponent<SpriteRenderer>().color = ih.rarityColors[(int)it.rarity];
            LargeIcon.GetChild(0).GetComponent<SpriteRenderer>().sprite = it.largeIcon;
        }
    }

}
