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

    public enum Functional //Добавлять для каждого предмета!!!!!
    {
        Blue,
        Black
    }

    public void SetFunctional() //Добавлять для каждого предмета!!!!!
    {
        switch (functional)
        {
            case Functional.Blue:
                ItemType = ActiveItem.CreateInstance<BlueScotch>();
                break;
            case Functional.Black:
                ItemType = ActiveItem.CreateInstance<BlackScotch>();
                break;

        }

        ItemType.icon = this.icon;
        ItemType.largeIcon = this.largeIcon;
        ItemType.character = this.character;
        ItemType.itemName = this.itemName;
        ItemType.description = this.description;
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


    public virtual void OnActivate()
    {

    }
}
