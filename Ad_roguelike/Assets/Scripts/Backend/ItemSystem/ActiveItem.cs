using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu]
public class ActiveItem : ScriptableObject
{
    public Character character;
    public string itemName;
    public Sprite icon, largeIcon;
    public string description;
    public Functional functional;
    public ActiveItem ItemType;
    public Type myType;
    public int UsesLeft, MaxUses, TotalTimesUsed;

    public enum Functional //��������� ��� ������� ��������!!!!!
    {
        blue,
        black,
        drops,
        cent,
        transistor,
        silver,
        sifter,
        oil,
        fish,
        itemdeletter,
        glue
    }
    public enum Type
    {
        Infinite,
        EveryFight,
        EveryStage

    }


    public static ActiveItem CreateItem(Functional f)
    {
        ActiveItem item;
        item = SwitchFunctional(f);
        return item;
    }
    static ActiveItem SwitchFunctional(Functional f) //��������� ��� ������� ��������!!!!!
    {
        switch (f)
        {
            case Functional.blue:
                return ActiveItem.CreateInstance<BlueScotch>();
            case Functional.black:
                return ActiveItem.CreateInstance<BlackScotch>();
            case Functional.drops:
                return ActiveItem.CreateInstance<Drops>();
            case Functional.cent:
                return ActiveItem.CreateInstance<Cent>();
            case Functional.transistor:
                return ActiveItem.CreateInstance<Transistor>();
            case Functional.silver:
                return ActiveItem.CreateInstance<Silver>();
            case Functional.sifter:
                return ActiveItem.CreateInstance<Sifter>();
            case Functional.oil:
                return ActiveItem.CreateInstance<Oil>();
            case Functional.fish:
                return ActiveItem.CreateInstance<Fish>();
            case Functional.itemdeletter:
                return ActiveItem.CreateInstance<ItemDeletter>();
            case Functional.glue:
                return ActiveItem.CreateInstance<Glue>();

        }
        return null;
    }
    public void SetFunctional()
    {
        ItemType = SwitchFunctional(functional);
        ItemType.icon = this.icon;
        ItemType.largeIcon = this.largeIcon;
        ItemType.character = this.character;
        ItemType.itemName = this.itemName;
        ItemType.description = this.description;
        ItemType.functional = this.functional;
        ItemType.myType = this.myType;
        ItemType.UsesLeft = this.UsesLeft;
        ItemType.MaxUses = this.MaxUses;
    }

    public void SetCharacter()
    {
        character = Camera.main.GetComponent<Character>();
    }
    public void SetCharacter(Character _ch)
    {
        character = _ch;
    }
    public void OnActivate()
    {
        if (character.CanUseActiveArts)
        {
            if (myType != Type.Infinite)
            {
                if (UsesLeft > 0)
                {
                    UsesLeft -= 1;
                    Activate();
                    TotalTimesUsed += 1;
                    character.AddStats(Stats.Statistics.ArtsUsed, 1);
                }
                else
                {
                    Debug.Log("������������� ���������");
                }
            }
            else
            {
                Activate();
                TotalTimesUsed += 1;
                character.AddStats(Stats.Statistics.ArtsUsed, 1);
            }
        }
        else
        {
            Debug.Log("���������");
        }

    }
    public virtual void Activate()
    {

    }
    public void OnFightEnd()
    {
        if (myType == Type.EveryFight)
        {
            UsesLeft = MaxUses;
        }
    }
    public void OnNewStage()
    {
        if(myType == Type.EveryStage)
        {
            UsesLeft = MaxUses;
        }
    }
    public bool Random(int chance)
    {
        if(UnityEngine.Random.Range(0, 100) < chance)
        {
            character.AddStats(Stats.Statistics.Luck, 1);
            return true;
        }
        character.AddStats(Stats.Statistics.Unluck, 1);
        return false;
    }
}
