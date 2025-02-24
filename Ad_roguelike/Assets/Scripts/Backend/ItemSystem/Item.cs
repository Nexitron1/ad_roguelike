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

    public enum Rarity
    {
        common,
        uncommon,
        rare,
        special
    }
    public enum Functional //��������� ��� ������� ��������!!!!!
    {
        Cuprum,
        Pen
    }
    public void SetFunctional(Rarity r) //��������� ��� ������� ��������!!!!!
    {
        switch (functional)
        {
            case Functional.Cuprum:
                ItemType = Item.CreateInstance<Cuprum>();
                ItemType.rarity = r;
                break;
            case Functional.Pen:
                ItemType = Item.CreateInstance<Pen>();
                ItemType.rarity = r;
                break;
        }
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
    public virtual void OnActiveArtUsed() { Debug.LogWarning("Non-realised"); } //��� ������������ ��� ����������
    public virtual void Init() { } //�����������
    public virtual void OnAdStart() { } //�����������
    public virtual void OnEachSec() { } //�����������
    public virtual void OnRoomEntry() { } //�����������
    public virtual void OnFightEnd() { } //�����������
    public virtual void OnEachFrame() { } //�����������
    //Debug.LogWarning("You're trying to use empty method, or there's no override");
}
