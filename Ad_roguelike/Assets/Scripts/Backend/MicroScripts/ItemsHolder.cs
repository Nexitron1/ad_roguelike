using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsHolder : MonoBehaviour
{
    public Item[] Items;
    public ActiveItem[] ActiveItems;
    public Color[] rarityColors;

    public Item CreateItem()
    {
        Item item;
        int r = Random.Range(0, Items.Length);
        item = Item.CreateItem(Items[r].functional);
        item.SetFunctional((Item.Rarity)Random.Range(0, 4));
        item.rarity = GetRarity();
        item.icon = Items[r].icon;
        item.largeIcon = Items[r].largeIcon;
        item.character = Items[r].character;
        item.itemName = Items[r].itemName;
        item.descriptions = Items[r].descriptions;
        item.functional = Items[r].functional;

        return item;
    }

    public ActiveItem CreateActiveItem()
    {
        ActiveItem item;
        int r = Random.Range(0, ActiveItems.Length);
        item = ActiveItem.CreateItem(ActiveItems[r].functional);
        item.SetFunctional();
        item.icon = ActiveItems[r].icon;
        item.largeIcon = ActiveItems[r].largeIcon;
        item.character = ActiveItems[r].character;
        item.itemName = ActiveItems[r].itemName;
        item.description = ActiveItems[r].description;
        item.functional = ActiveItems[r].functional;
        item.myType = ActiveItems[r].myType;
        item.UsesLeft = ActiveItems[r].UsesLeft;
        item.MaxUses = ActiveItems[r].MaxUses;

        return item;
    }

    Item.Rarity GetRarity()
    {
        int r = Random.Range(0, 100);

        if (r < 5)
            return Item.Rarity.special;
        
        if (r < 5 + 15)
            return Item.Rarity.rare;
        if (r < 5 + 15 + 35)
            return Item.Rarity.uncommon;

        return Item.Rarity.common;
    }
}
