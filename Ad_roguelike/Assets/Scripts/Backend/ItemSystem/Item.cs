using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "baseItem")]
public class Item : ScriptableObject
{
    public Character character;
    public string itemName;
    public Sprite icon, largeIcon;
    public string[] descriptions;
    public Functional functional;
    public Item ItemType;


    public Rarity rarity;
    /*
     * switch (rarity)
        {
            case Rarity.common:
                
                break;
            case Rarity.uncommon:

                break;
            case Rarity.rare:

                break;
            case Rarity.special:

                break;
        }
     */
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        special
    }
    public enum Functional //Добавлять для каждого предмета!!!!!
    {
        cuprum,
        pen,
        antivirus,
        medali,
        arctic,
        printerbox,
        key,
        kurevo,
        chicken,
        furnace,
        dirt
    }
    public static Item CreateItem(Functional func)
    {
        Item tmp;
        tmp = SwitchFunctional(func);


        return tmp;
    }
    public static Item SwitchFunctional(Functional f) //Добавлять для каждого предмета!!!!!
    {
        switch (f)
        {
            case Functional.cuprum:
                return Item.CreateInstance<Cuprum>();
            case Functional.pen:
                return Item.CreateInstance<Pen>();
            case Functional.antivirus:
                return Item.CreateInstance<AntiVirus>();
            case Functional.medali:
                return Item.CreateInstance<Medali>();
            case Functional.arctic:
                return Item.CreateInstance<Arctic>();
            case Functional.printerbox:
                return Item.CreateInstance<PrinterBox>();
            case Functional.key: 
                return Item.CreateInstance<Key>();
            case Functional.kurevo:
                return Item.CreateInstance<Kurevo>();
            case Functional.chicken: 
                return Item.CreateInstance<Chicken>();
            case Functional.furnace:
                return Item.CreateInstance<Furnace>();
            case Functional.dirt:
                return Item.CreateInstance<Dirt>();
        }
        return null;
    }
    public void SetFunctional(Rarity r)
    {
        ItemType = SwitchFunctional(functional);
        ItemType.rarity = r;
        ItemType.icon = this.icon;
        ItemType.largeIcon = this.largeIcon;
        ItemType.character = this.character;
        ItemType.itemName = this.itemName;
        ItemType.descriptions = this.descriptions;
        ItemType.functional = this.functional;
    }
    public void SetCharacter()
    {
        character = Camera.main.GetComponent<Character>();
    }
    public void SetCharacter(Character _ch)
    {
        character = _ch;
    }
    public virtual void OnActiveArtUsed(int index) {} 
    public virtual void Init() { SetCharacter(); } 
    public virtual void OnAdStart() { } 
    public virtual void OnEachSec() { } 
    public virtual void OnRoomEntry() { } 
    public virtual void OnFightEnd() { } 
    public virtual void OnEachFrame() { } 
    public virtual void OnNewStage()
    {

    }
    public bool Random(int chance)
    {
        int r = UnityEngine.Random.Range(0, 100);

        if(r < chance)
        {
            character.AddStats(Stats.Statistics.Luck, 1);
            return true;
        }
        character.AddStats(Stats.Statistics.Unluck, 1);
        return false;
    }
    //Debug.LogWarning("You're trying to use empty method, or there's no override");
}
