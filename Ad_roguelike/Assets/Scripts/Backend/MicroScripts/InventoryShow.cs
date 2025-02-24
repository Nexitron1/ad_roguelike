using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryShow : MonoBehaviour
{
    [SerializeField] Color[] rarityColors;
    [SerializeField] TMP_Text Name, Good, Bad;
    [SerializeField] Transform LargeIcon;
    Transform[] slots = new Transform[18];
    Item[] items = new Item[18];
    int itemsCount = 0;
    int Selected = -1;
    Character character;

    private void Start()
    {
        character = Camera.main.GetComponent<Character>();
        FillSlots();
        //AddItem();
        character.InventorySet(this);
        SelectItem(0);
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
        items[itemsCount] = item;
        slots[itemsCount].GetChild(0).GetComponent<SpriteRenderer>().sprite = item.icon;
        switch (item.rarity) 
        {
            case Item.Rarity.common:
                slots[itemsCount].GetComponent<SpriteRenderer>().color = rarityColors[0];
                break;
            case Item.Rarity.uncommon:
                slots[itemsCount].GetComponent<SpriteRenderer>().color = rarityColors[1];
                break;
            case Item.Rarity.rare:
                slots[itemsCount].GetComponent<SpriteRenderer>().color = rarityColors[2];
                break;
            case Item.Rarity.special:
                slots[itemsCount].GetComponent<SpriteRenderer>().color = rarityColors[3];
                break;
        
        }

        itemsCount++;
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
            if(t.Length > 1)
                Bad.text = t[1];

            LargeIcon.GetComponent<SpriteRenderer>().color = rarityColors[(int)it.rarity];
            LargeIcon.GetChild(0).GetComponent<SpriteRenderer>().sprite = it.largeIcon;
        }
    }

}
