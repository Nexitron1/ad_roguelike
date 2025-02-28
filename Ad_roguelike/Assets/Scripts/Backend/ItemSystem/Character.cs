using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.Progress;

public class Character : MonoBehaviour
{
    public Item.Rarity[] TestRarity;
    public List<Item> items = new List<Item>();
    public List<ActiveItem> activeItems = new List<ActiveItem>();
    public float TesttimeMultiplier = 1;
    public float sliderPos = 1;
    public float Health = 50, MaxHealth = 50;
    public float MultLeft = 1f, MultRight = 1f;

    public MapGenerator generator;
    public Movement cosmonaut;
    public Vector2Int CosmonautPos = Vector2Int.zero;

    public Icon Ad1;
    public float Damage = 1f;
    void Start()
    {
        generator = GetComponent<MapGenerator>();
        RecreateItems();
        FirstTimeForItems();
        StartCoroutine(DamageTimer());
    }
    public void SetMovement(Movement m)
    {
        cosmonaut = m;
    } 
    public void PlusMults(float left, float right)
    {
        MultLeft += left;
        MultRight += right;

        if(MultLeft <= 0)
        {
            MultLeft = 0.000001f;
        }
        if (MultRight <= 0)
        {
            MultRight = 0.000001f;
        }
    }
    public void SkipAd()
    {
        skipAd = true;
    }
    InventoryShow inventory;
    public void InventorySet(InventoryShow inv)
    {
        inventory = inv;
        if (inventory != null)
        {
            foreach (Item item in items)
                inventory.AddItem(item.ItemType);
        }
    }
    Hotbar hotbar;
    public void HotbarSet(Hotbar _hotbar)
    {
        hotbar = _hotbar;
        if(hotbar != null)
        {
            foreach(ActiveItem actItem in activeItems)
            {
                hotbar.AddItem(actItem.ItemType);
            }
        }
    }
    void RecreateItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {

                //Item.Rarity r = (Item.Rarity)Random.Range(0, 4);
                Item.Rarity r = TestRarity[i];
                if (items[i].ItemType != null)
                {
                    Debug.Log("They have");
                    r = items[i].ItemType.rarity;
                }
                items[i] = Instantiate(items[i]);
                items[i].SetCharacter(this);
                items[i].SetFunctional(r);
            }
        }

        for (int i = 0; i < activeItems.Count; i++)
        {
            if(activeItems[i] != null)
            {
                activeItems[i] = Instantiate(activeItems[i]);
                activeItems[i].SetCharacter(this);
                activeItems[i].SetFunctional();
            }
        }
    }
    void FirstTimeForItems()
    {
        foreach (Item item in items)
        {
            OnFirstTime(item);
            
        }
    }

    public void RoomEntry(MapGenerator.RoomTypes roomType)
    {
        OnRoomEnter();
        switch (roomType)
        {
            case MapGenerator.RoomTypes.Fight:
                Ad1.StartApp();
                break;
            case MapGenerator.RoomTypes.Boss:
                break;
            case MapGenerator.RoomTypes.Shop:
                break;
            case MapGenerator.RoomTypes.Treasure:
                break;
        }
    }
    void Update()
    {
        
        if (Health > MaxHealth) Health = MaxHealth;
        if (ad != null)
        {
            ad.TimerSecondsText.text = time.ToString("0");
            ad.Duration.value = time;
            ad.Duration.maxValue = endTime;
            ad.M1Text.text = MultLeft.ToString("0.00");
            ad.M2Text.text = MultRight.ToString("0.00");
            ad.hp.value = Health;
            ad.hp.maxValue = MaxHealth;
            ad.MaxHp.text = MaxHealth.ToString();
            ad.DurationTime.text = endTime.ToString();
            
        }


        foreach (Item item in items)
        {
            item.ItemType.OnEachFrame();
        }


    }



    float time = 0, endTime = 0;
    Ad ad = null;



    bool canBeDamaged = false;
    bool endlessTimer = true;
    public void StartTimer(Ad _ad, float Duration)
    {
        if (ad == null)
        {
            ad = _ad;
            endTime = Duration;
            StartCoroutine(Timer());
        }
    }

    public void AddItem(Item item, Item.Rarity rarity)
    {
        items.Add(Instantiate(item));
        items[items.Count - 1].SetCharacter(this);
        items[items.Count - 1].SetFunctional(rarity);
        OnFirstTime(item);
    }

    public void SkipTimeByDurationAndPlace(float duration, string place)
    {
        if(place == "start")
        {
            if (time < duration)
                time = duration;
        }
        else if(place == "end")
        {
            endTime -= duration; 
        }
        else if (place == "now")
        {
            time += duration;
        }
    }
    public void SkipTimeByPercentAndPlace(float percent, string place)
    {
        if (place == "start")
        {
            time += (endTime - time) * percent;
        }
        else if (place == "end")
        {
            endTime -= endTime * percent;
        }
        else if  (place == "now")
        {
            time += percent * endTime;
        }
    }
    bool TimerStarted = false, skipAd = false;
    IEnumerator Timer()
    {
        if (TimerStarted == false)
        {
            TimerStarted = true;
            canBeDamaged = true;
            OnAdStarts();
            yield return null;
            float sec = 0;
            while (time < endTime && !skipAd)
            {
                if (time < endTime / 2)
                {
                    yield return new WaitForSeconds(TesttimeMultiplier / MultLeft);
                    time += TesttimeMultiplier;
                }
                else
                {
                    yield return new WaitForSeconds(TesttimeMultiplier / MultRight);
                    time += TesttimeMultiplier;
                }

                sec += TesttimeMultiplier;
                if (sec >= 1)
                {
                    OnEachSec();
                    sec = 0;
                }
            }
            canBeDamaged = false;
            OnFightEnd();
            time = 0;
            if (ad != null)
            {
                ad.transform.parent.parent.GetComponent<Window>().CloseWindow();
                Debug.Log("Ad closed Automaticly");
            }
            ad = null;
            skipAd = false;
            TimerStarted = false;
        }
    }


    IEnumerator DamageTimer()
    {
        while (endlessTimer)
        {
            if (canBeDamaged)
            {
                yield return new WaitForSeconds(1);
                Health -= Damage;
            }
            else
            {
                yield return null;
            }
        }
        
    }








    public void OnActiveArtUsed()
    {
        foreach(Item item in items)
        {
            item.ItemType.OnActiveArtUsed();
        }
    }
    public void OnEachSec()
    {
        foreach (Item item in items) 
        {
            item.ItemType.OnEachSec();
        }
    }
    public void OnFightEnd()
    {
        if (cosmonaut != null)
            cosmonaut.CanMove = true;
        if (generator != null)
        {
            generator.rooms[CosmonautPos] = MapGenerator.RoomTypes.Empty;
            generator.RefreshIcons();
        }
        foreach (Item item in items)
        {
            item.ItemType.OnFightEnd();
        }
    }
    public void OnFirstTime(Item item)
    {
        item.ItemType.Init();
    }
    public void OnRoomEnter()
    {
        foreach (Item item in items)
        {
            item.ItemType.OnRoomEntry();
        }
    }
    public void OnAdStarts()
    {
        if (cosmonaut != null)
            cosmonaut.CanMove = false;
        foreach (Item item in items)
        {
            item.ItemType.OnAdStart();
        }
    }

    
}
