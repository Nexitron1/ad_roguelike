using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsHolder : MonoBehaviour
{
    public Item[] Items;
    public ActiveItem[] ActiveItems;
    public AdFeature[] AdFeatures;
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

    public Item CreateItem(Item.Functional f)
    {
        Item item;
        int r = -1;  
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].functional == f)
            {
                r = i; 
                break;
            }
        }
        item = Item.CreateItem(f);
        item.SetFunctional(GetRarity());
        item.rarity = GetRarity();
        item.icon = Items[r].icon;
        item.largeIcon = Items[r].largeIcon;
        item.character = Items[r].character;
        item.itemName = Items[r].itemName;
        item.descriptions = Items[r].descriptions;
        item.functional = Items[r].functional;

        return item;
    }

    public Item CreateItem(Item.Functional f, Item.Rarity rarity)
    {
        Item item;
        int r = -1;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].functional == f)
            {
                r = i;
                break;
            }
        }
        item = Item.CreateItem(f);
        item.SetFunctional(rarity);
        item.rarity = rarity;
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

    public ActiveItem CreateActiveItem(ActiveItem.Functional f)
    {
        ActiveItem item;
        int r = -1;
        for (int i = 0; i < ActiveItems.Length; i++)
        {
            if (ActiveItems[i].functional == f)
            {
                r = i;
                break;
            }
        }
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

    public AdFeature CreateAdFeature()
    {
        AdFeature feature;
        int r = Random.Range(0, AdFeatures.Length);
        feature = AdFeature.CreateFeature(AdFeatures[r].myType);
        feature.myType = AdFeatures[r].myType;
        feature.myDiff = GetDiff();
        feature.AdName = AdFeatures[r].AdName;
        feature.diffDescs = AdFeatures[r].diffDescs;
        return feature;

    }
    public AdFeature CreateAdFeature(AdFeature.AdType t)
    {
        AdFeature feature;
        int r = -1;
        for (int i = 0; i < AdFeatures.Length; i++)
        {
            if (t == AdFeatures[i].myType)
            {
                r = i; 
                break;
            }
        }
        feature = AdFeature.CreateFeature(AdFeatures[r].myType);
        feature.myType = AdFeatures[r].myType;
        feature.myDiff = GetDiff();
        feature.AdName = AdFeatures[r].AdName;
        feature.diffDescs = AdFeatures[r].diffDescs;
        return feature;
    }
    public AdFeature CreateAdFeature(AdFeature.AdType t, AdFeature.Difficulty d)
    {
        AdFeature feature;
        int r = -1;
        for (int i = 0; i < AdFeatures.Length; i++)
        {
            if (t == AdFeatures[i].myType)
            {
                r = i;
                break;
            }
        }
        feature = AdFeature.CreateFeature(AdFeatures[r].myType);
        feature.myType = AdFeatures[r].myType;
        feature.myDiff = d;
        feature.AdName = AdFeatures[r].AdName;
        feature.diffDescs = AdFeatures[r].diffDescs;
        return feature;
    }
    public AdFeature CreateAdFeature(AdFeature.Difficulty d)
    {
        AdFeature feature;
        int r = Random.Range(0, AdFeatures.Length);
        feature = AdFeature.CreateFeature(AdFeatures[r].myType);
        feature.myType = AdFeatures[r].myType;
        feature.myDiff = d;
        feature.AdName = AdFeatures[r].AdName;
        feature.diffDescs = AdFeatures[r].diffDescs;
        return feature;

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
    AdFeature.Difficulty GetDiff()
    {
        int r = Random.Range(0, 100);

        if (r < 5)
            return AdFeature.Difficulty.extreme;
        if (r < 5 + 15)
            return AdFeature.Difficulty.hard;
        if (r < 5 + 15 + 35)
            return AdFeature.Difficulty.normal;

        return AdFeature.Difficulty.easy;
    }
}
