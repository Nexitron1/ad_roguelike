using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Stats;

public class Character : MonoBehaviour
{
    public Item.Rarity[] TestRarity;
    public List<Item> items = new List<Item>();
    public List<ActiveItem> activeItems = new List<ActiveItem>();
    public AdFeature.Difficulty[] TestDifficulty;
    public List<AdFeature> AdFeatures = new List<AdFeature>();
    public float TesttimeMultiplier = 1;
    public float sliderPos = 1;
    public float Health { get; private set; } = 50;
    public float MaxHealth = 50;
    public float OverHealth = 0;
    public float MultLeft = 1f, MultRight = 1f;

    public MapGenerator generator;
    public Movement cosmonaut;
    public Vector2Int CosmonautPos;

    public Icon Ad1, IconMap;
    public Icon Treasure, Icon_Shop, Terminal;
    public float BaseAdLengh = 10;
    public int stage = 1;
    public float Damage = 1f;

    public float ShopMultiplier = 1;
    public int ShopAdder = 0;
    public int money = 0;
    public List<TimeSkip> TimeSkipsQueue = new List<TimeSkip>(); //
    public bool CanUseActiveArts = true;

    public bool cheats = false;
    
    void Start()
    {
        StatisticValues = new float[System.Enum.GetNames(typeof(Stats.Statistics)).Length];
        generator = GetComponent<MapGenerator>();
        RecreateItems();
        FirstTimeForItems();

        for (int i = 0; i < AdFeatures.Count; i++)
        {
            if (AdFeatures[i] != null)
            {
                AdFeature.Difficulty d = TestDifficulty[i];
                AdFeatures[i] = GetComponent<ItemsHolder>().CreateAdFeature(AdFeatures[i].myType, d);
                AdFeatures[i].InitType(d);
            }
        }

    }
    
    public void Heal(float hp)
    {
        if (hp >= 0)
        {
            var tmp = Health;
            Health += hp;
            if (Health > MaxHealth)
                Health = MaxHealth;

            tmp = Health - tmp;

            if (tmp < 0)
                tmp = 0;

            AddStats(Statistics.HpHealed, tmp);
        }
        else
        {
            var tmp = OverHealth;
            OverHealth += hp;
            if (OverHealth < 0)
            {
                OverHealth = 0;
                Health += hp + tmp;
            }
        }
    }
    public void DeleteItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items.RemoveAt(index);
            RefreshInventory();

            ReactivateItems();
            AddStats(Stats.Statistics.ItemsDeleted, 1);
        }
    }
    List<float> f = new List<float>();
    public void DeleteActiveItem(int index)
    {
        if(index >= 0 && index < activeItems.Count)
        {
            activeItems.RemoveAt(index);
            RefreshHotbar();
            AddStats(Stats.Statistics.ItemsDeleted, 1);
        }
    }
    void ReactivateItems()
    {
        var tmp = MaxHealth;
        MaxHealth = BaseValues.MaxHealth;
        //Health += MaxHealth - tmp;
        MultLeft = BaseValues.MultLeft;
        MultRight = BaseValues.MultRight;
        BaseAdLengh = BaseValues.BaseAdLengh;
        BossTimeAdder = BaseValues.BossTimeAdder;
        Damage = BaseValues.Damage;
        ShopMultiplier = BaseValues.ShopMultiplier;
        ShopAdder = BaseValues.ShopAdder;
        CanUseActiveArts = BaseValues.CanUseActiveArts;
        MoneyMultiplier = BaseValues.MoneyMultiplier;
        MoneyAdder = BaseValues.MoneyAdder;
        CanRegenerateOnNewStage = BaseValues.CanRegenerateOnNewStage;
        TimeSkipsQueue = null;
        TimeSkipsQueue = new List<TimeSkip>();

        generator.ShopChance = new List<int> { 100 };
        generator.TreasureChance = new List<int> { 100 };

        PlusMults(GetStats(Statistics.FishUsed) * 0.25f, GetStats(Statistics.FishUsed) * 0.25f);

        foreach(Item item in items)
        {
            OnFirstTime(item);
        }
        for (int i = 0; i < AdFeatures.Count; i++)
        {
            AdFeatures[i].Init();
        }



        if (TimerStarted)
        {
            OnAdStarts();
            endTime = BaseAdLengh + 10 * stage;
        }
    }
    public Stats stats;
    float[] StatisticValues;
    public void SetStats(Stats s)
    {
        stats = s;
        for (int i = 0; i < StatisticValues.Length; i++)
        {
            stats.Modify(i, StatisticValues[i]);
        }
    }
    public void AddStats(Stats.Statistics s, float value)
    {
        StatisticValues[(int)s] += value;
        if(stats != null)
        {
            stats.Modify((int)s, StatisticValues[(int)s]);
        }

    }
    public float GetStats(Statistics s)
    {
        return StatisticValues[(int)s];
    }
    public float GetStats(int s)
    {
        return StatisticValues[s];
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
    public void RefreshInventory()
    {
        if(inventory != null)
        {
            inventory.RefreshIcons();
            inventory.items = new Item[18];
            inventory.itemsCount = 0;
            foreach (Item item in items)
                inventory.AddItem(item.ItemType);
            inventory.SelectItem(0);
        }
        else
        {
            Debug.LogWarning("InventoryIsNull but it's okay");
        }
    }
    public void RefreshHotbar()
    {
        if(hotbar != null)
        {
            hotbar.Refresh();
            hotbar.items.Clear();
            foreach (ActiveItem actItem in activeItems)
            {
                hotbar.AddItem(actItem.ItemType);
            }
        }
        else
        {
            Debug.LogWarning("HotbarIsNull but it's okay");
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
    public MapGenerator.RoomTypes GetRoomType()
    {
        return generator.rooms[CosmonautPos];
    }
    void FirstTimeForItems()
    {
        foreach (Item item in items)
        {
            OnFirstTime(item);
            
        }
    }
    public float BossTimeAdder;
    public void RoomEntry(MapGenerator.RoomTypes roomType)
    {
        OnRoomEnter();
        switch (roomType)
        {
            case MapGenerator.RoomTypes.Fight:
                StartCoroutine(WaitOneFrame(BaseAdLengh + 10 * stage));
                break;
            case MapGenerator.RoomTypes.Boss:
                boss = 1;
                StartCoroutine(WaitOneFrame(BaseAdLengh + 10 * stage + BossTimeAdder));
                break;
            case MapGenerator.RoomTypes.Shop:
                Icon_Shop.StartApp();
                CanMove = false;
                break;
            case MapGenerator.RoomTypes.Treasure:
                Treasure.StartApp();
                break;
        }
    }
    IEnumerator WaitOneFrame(float a)
    {
        Ad1.StartApp();
        yield return null;
        StartTimer(a);
    }
    TerminalTemplate tt;
    public void SetTT(TerminalTemplate t)
    {
        tt = t;
    }
    public bool Defeated = false;
    public GameObject RestartButton, Cracks2;
    public UnityEngine.UI.Image WallPaper;
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public Sprite Cracks;
    IEnumerator Def()
    {
        yield return null;
        WallPaper.sprite = Cracks;
        Cracks2.SetActive(true);
        Health = MaxHealth;
        string me = "\ncongrats, you died\nyou can continue playing, but our company is not responsible for the possible consequences.\nThe restart button is now in the lower right corner.\n";
        for (int i = 0; i < me.Length; i++)
        {
            tt.content += me[i];
            yield return new WaitForSecondsRealtime(0.025f);
        }
        RestartButton.SetActive(true);
        
    }
    void Update()
    {
        
        if (Health > MaxHealth) Health = MaxHealth;
        if (OverHealth > 0 && Health < MaxHealth)
        {
            float temp = MaxHealth - Health;
            Health = MaxHealth;
            OverHealth -= temp;
        }
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        {
            Terminal.StartApp();       
        }
        if (Health <= 0 && !Defeated)
        {
            Defeated = true;
            //Debug.Log("Всё, ты умер");
            //SceneManager.LoadScene(2);
            Terminal.StartApp();
            StartCoroutine(Def());  
        }
        if (PlayingAd)
        {
            Timer2();
        }
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

        if (stage >= 4 && !Defeated)
        {
            SceneManager.LoadScene(1);
        }


    }


    public float time = 0, endTime = 0;
    public Ad ad = null;

    bool canBeDamaged = false;
    public void StartTimer(float Duration)
    {
        endTime += Duration;
        //StartCoroutine(Timer());
        InitAdPlayer();
        PlayingAd = true;
    }
    public void ClearRoom()
    {
        if (generator != null)
        {
            generator.rooms[CosmonautPos] = MapGenerator.RoomTypes.Empty;
            generator.RefreshIcons();
        }
    }

    public void SetRoom(MapGenerator.RoomTypes type)
    {
        generator.rooms[CosmonautPos] = type;
        generator.RefreshIcons();
        RoomEntry(type);
    }

    public bool TryAddItem()
    {
        if(items.Count < 18)
        {
            return true;
        }
        return false;
    }
    public void AddItem(Item item, Item.Rarity rarity)
    {
        if (items.Count < 18)
        {
            items.Add(Instantiate(item));
            items[items.Count - 1].SetCharacter(this);
            items[items.Count - 1].SetFunctional(rarity);
            OnFirstTime(items[items.Count - 1]);
            RefreshInventory();
        }
        else
        {
            Debug.LogError("Reached limit of items");
        }
    }
    public void AddItem(Item item)
    {
        if (items.Count < 18)
        {
            items.Add(Instantiate(item));
            items[items.Count - 1].SetCharacter(this);
            items[items.Count - 1].SetFunctional(item.rarity);
            OnFirstTime(items[items.Count - 1]);
            RefreshInventory();
        }
        else
        {
            Debug.LogError("Reached limit of items");
        }
    }




    public bool TryAddActiveItem()
    {
        if (activeItems.Count < 2)
        {
            return true;
        }
        return false;
    }
    public void AddActiveItem(ActiveItem item)
    {
        if(activeItems.Count < 2)
        {
            activeItems.Add(Instantiate(item));
            activeItems[activeItems.Count - 1].SetCharacter(this);
            activeItems[activeItems.Count - 1].SetFunctional();
            RefreshHotbar();
        }
        else
        {
            Debug.LogError("Reached limit of ActiveItems");
        }
    }
    public void OldAdFeature()
    {
        AdFeatures.Add(GetComponent<ItemsHolder>().CreateAdFeature());
        ad.RefreshFeatures();
    }

    public void SkipTime(float duration, TimeSkip.DurationType dt, float place, TimeSkip.PlaceType pt, bool cond = true)
    {
        TimeSkipsQueue.Add(new TimeSkip(duration, dt, place, pt, cond));
    }
    public void DenyTime(float duration, bool percent = false)
    {
        if (!percent)
            time -= duration;
        else
            time -= duration * endTime;
    } 
    bool TimerStarted = false, skipAd = false;

    int boss = 0;
    int AdFeaturesCount()
    {
        return stage + Random.Range(0, 2) + Random.Range(0, 2) + boss;
    }

    bool PlayingAd = false;
    float sec;

    void InitAdPlayer()
    {
        canBeDamaged = true;
        OnAdStarts();
        AdFeatures.Clear();
        var tmp1 = AdFeaturesCount();
        for (int i = 0; i < tmp1; i++)
        {
            AdFeatures.Add(GetComponent<ItemsHolder>().CreateAdFeature());
        }

        for (int i = 0; i < AdFeatures.Count; i++)
        {
            AdFeatures[i].Init();
        }
        ad.RefreshFeatures();
    }
    public bool autoSkip = false;
    void Timer2()
    {
        if (autoSkip) skipAd = true;
        if ((time < endTime) && !skipAd)
        {
            if (time < endTime / 2)//первая половина
            {
                //yield return new WaitForSeconds(TesttimeMultiplier / MultLeft); //срочно фиксить
                time += Time.deltaTime * MultLeft;
            }
            else//вторая
            {
                //yield return new WaitForSeconds(TesttimeMultiplier / MultRight); //срочно фиксить
                time += Time.deltaTime * MultRight;
            }

            sec += Time.deltaTime;
            if (sec >= 1)
            {
                OnEachSec();
                if (canBeDamaged) Heal(-Damage);
                sec = 0;
            }

            float skip = TimeSkipMax();
            time += skip;
        }
        else
        {


            canBeDamaged = false;
            time = 0;
            if (ad != null)
            {
                ad.transform.parent.parent.GetComponent<Window>().CloseWindow();
                Debug.Log("Ad closed Automaticly");
            }
            ad = null;
            skipAd = false;
            TimerStarted = false;
            boss = 0;
            PlayingAd = false;
            OnFightEnd();
        }

    }

    private float TimeSkipMax()
    {
        List<float> tmp_time = new List<float>();

        for (int l = TimeSkipsQueue.Count - 1; l >= 0; l--)
        {
            var t = TimeSkipsQueue[l];
            if (t.conditional)
            {
                if (t.durationType == TimeSkip.DurationType.absolute)
                {
                    if (t.placeType == TimeSkip.PlaceType.absolute)
                    {
                        if (t.place > 0)
                        {
                            if (time >= t.place)
                            {
                                if (time >= t.place + t.duration)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place < 0)
                        {
                            if (time >= endTime + t.place)
                            {
                                if (time >= endTime + t.place + t.duration)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place == 0)
                        {
                            if (time >= t.place + t.duration)
                            {
                                TimeSkipsQueue.Remove(t);
                            }
                            else
                            {
                                tmp_time.Add(t.duration);
                                TimeSkipsQueue.Remove(t);
                            }
                        }
                    }
                    else if (t.placeType == TimeSkip.PlaceType.percent)
                    {
                        if (t.place > 0)
                        {
                            if (time >= t.place * endTime)
                            {
                                if (time >= t.place * endTime + t.duration)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place < 0)
                        {
                            if (time >= endTime + t.place * endTime)
                            {
                                if (time >= endTime + t.place * endTime + t.duration)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place == 0)
                        {
                            if (time >= t.place * endTime + t.duration)
                            {
                                TimeSkipsQueue.Remove(t);
                            }
                            else
                            {
                                tmp_time.Add(t.duration);
                                TimeSkipsQueue.Remove(t);
                            }
                        }
                    }
                }
                else if (t.durationType == TimeSkip.DurationType.percent)
                {
                    if (t.placeType == TimeSkip.PlaceType.absolute)
                    {
                        if (t.place > 0)
                        {
                            if (time >= t.place)
                            {
                                if (time >= t.place + t.duration * endTime)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration * endTime);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place < 0)
                        {
                            if (time >= endTime + t.place)
                            {
                                if (time >= endTime + t.place + t.duration * endTime)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration * endTime);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place == 0)
                        {
                            if (time >= t.place + t.duration * endTime)
                            {
                                TimeSkipsQueue.Remove(t);
                            }
                            else
                            {
                                tmp_time.Add(t.duration * endTime);
                                TimeSkipsQueue.Remove(t);
                            }
                        }
                    }
                    else if (t.placeType == TimeSkip.PlaceType.percent)
                    {
                        if (t.place > 0)
                        {
                            if (time >= t.place * endTime)
                            {
                                if (time >= t.place * endTime + t.duration * endTime)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration * endTime);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place < 0)
                        {
                            if (time >= endTime + t.place * endTime)
                            {
                                if (time >= endTime + t.place * endTime + t.duration * endTime)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration * endTime);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                        else if (t.place == 0)
                        {
                            if (time >= t.place * endTime)
                            {
                                if (time >= t.place * endTime + t.duration * endTime)
                                {
                                    TimeSkipsQueue.Remove(t);
                                }
                                else
                                {
                                    tmp_time.Add(t.duration * endTime);
                                    TimeSkipsQueue.Remove(t);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (t.durationType == TimeSkip.DurationType.absolute)
                {
                    tmp_time.Add(t.duration);
                    TimeSkipsQueue.Remove(t);
                }
                else if(t.durationType == TimeSkip.DurationType.percent)
                {
                    tmp_time.Add(t.duration * endTime);
                    TimeSkipsQueue.Remove(t);
                }
            }
        }

        float skip = 0;
        for (int i = 0; i < tmp_time.Count; i++)
        {
            if(i == 0)
            {
                skip = tmp_time[i];
                continue;
            }

            if (tmp_time[i] > skip)
            {
                skip = tmp_time[i];
            }
        }
        //Debug.Log("skip = " + skip);
        return skip;
    }

    public void OnActiveArtUsed(int index)
    {
        foreach(Item item in items)
        {
            item.ItemType.OnActiveArtUsed(index);
        }
        for (int i = 0; i < AdFeatures.Count; i++)
        {
            AdFeatures[i].OnActiveArtUsed(index);
        }
    }
    public void OnEachSec()
    {
        AddStats(Stats.Statistics.Waiting, 1);
        foreach (Item item in items) 
        {
            item.ItemType.OnEachSec();
        }
        for (int i = 0; i < AdFeatures.Count; i++)
        {
            AdFeatures[i].OnEachSec();
        }
    }
    public bool CanMove = true;
    public float MoneyMultiplier = 0.5f, MoneyAdder = 0;
    public bool CanRegenerateOnNewStage = true;

    public bool CanBuyItems = true;

    public void OnFightEnd()
    {
        money += (int)(endTime * MoneyMultiplier + MoneyAdder);
        AddStats(Stats.Statistics.MoneyRecived, (int)(endTime * MoneyMultiplier + MoneyAdder));
        endTime = 0;
        CanMove = true;
        foreach (AdFeature feature in AdFeatures) 
        {
            feature.OnFigthEnd();
        }
        AdFeatures.Clear();
        if (generator != null)
        {
            if (generator.rooms[CosmonautPos] == MapGenerator.RoomTypes.Boss)
            {
                stage += 1;
                CosmonautPos = Vector2Int.zero;
                cosmonaut.RefreshPos();
                OnNewStage();
                generator.ReConnect();
                generator.ClearArrays();
                generator.GenerateMap();
                IconMap.StartApp();
                if (CanRegenerateOnNewStage)
                {
                    Health = MaxHealth;
                }
                foreach (ActiveItem activeItem in activeItems)
                {
                    activeItem.ItemType.OnNewStage();
                }


            }
            else
            {
                generator.rooms[CosmonautPos] = MapGenerator.RoomTypes.Empty;
                generator.RefreshIcons();
            }

            
        }

        foreach (Item item in items)
        {
            item.ItemType.OnFightEnd();
        }
        foreach(ActiveItem activeItem in activeItems)
        {
            activeItem.ItemType.OnFightEnd();
        }

        AddStats(Stats.Statistics.AdWatched, 1);
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
        CanMove = false;
        foreach (Item item in items)
        {
            item.ItemType.OnAdStart();
        }
    }
    public void OnNewStage()
    {
        foreach(ActiveItem activeItem in activeItems)
        {
            activeItem.OnNewStage();
        }
        foreach(Item item in items)
        {
            item.ItemType.OnNewStage();
        }
    }

    
}


public class TimeSkip
{
    public readonly float duration;
    public readonly float place;
    public readonly DurationType durationType;
    public readonly PlaceType placeType;
    public readonly bool conditional = true;
    
    public enum PlaceType
    {
        absolute,
        percent
    }

    public enum DurationType
    {
        absolute,
        percent
    }

    public TimeSkip(float duration, DurationType dt, float place, PlaceType pt, bool conditional = true)
    {
        this.duration = duration;
        this.place = place;
        this.durationType = dt;
        this.placeType = pt;
        this.conditional = conditional;
    }
}



//Софтлок когда закрываешь карту во время движения
