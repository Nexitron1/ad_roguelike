using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Transform[] slots;
    public Transform[] slots2;
    public TMP_Text rerollPrice, Price;
    Item[] Items = new Item[3];
    int[] price = new int[6];
    public MicroIcons[] mic;
    ActiveItem[] ActiveItems = new ActiveItem[1];
    Character ch;
    ItemsHolder ih;
    int rerollsCount = 0;
    public Sprite checkmark;
    public Animator Kupuropriemnik;
    public Button BuyButton;

    public TMP_Text ActPass, Name, Desc, Good, Bad; 
    public void Reroll()
    {
        ch.Heal(ch.MaxHealth * 0.1f * rerollsCount);
        rerollsCount++;
        RandomizeItems();
        SetPrice();
        RefreshIcons();

        for (int i = 0; i < mic.Length; i++)
        {
            mic[i].Buyed = false;
        }
        Select(Selected);

    }
    void SetPrice()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (Items[i].rarity)
            {
                case Item.Rarity.common:
                    price[i] = (int)(30 * ch.ShopMultiplier + ch.ShopAdder);
                    break;
                case Item.Rarity.uncommon:
                    price[i] = (int)(40 * ch.ShopMultiplier + ch.ShopAdder);
                    break;
                case Item.Rarity.rare:
                    price[i] = (int)(55 * ch.ShopMultiplier + ch.ShopAdder);
                    break;
                case Item.Rarity.special:
                    price[i] = (int)(75 * ch.ShopMultiplier + ch.ShopAdder);
                    break;

            }
        }
        price[3] = (int)(55 * ch.ShopMultiplier + ch.ShopAdder);
        price[4] = 15;
        price[5] = 25;
        rerollPrice.text = (rerollsCount * -10).ToString() + "%";
    }
    void RandomizeItems()
    {
        for (int i = 0; i < ActiveItems.Length; i++)
        {
            ActiveItems[i] = ih.CreateActiveItem();
        }
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i] = ih.CreateItem();
        }
    }
    void RefreshIcons()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().sprite = Items[i].icon;
            slots[i].parent.GetComponent<Image>().color = ih.rarityColors[(int)Items[i].rarity];
        }
        for (int i = 0; i < slots2.Length; i++)
        {
            slots2[i].GetComponent<Image>().sprite = ActiveItems[i].icon;
            slots2[i].parent.GetComponent<Image>().color = Color.red;
        }
    }
    int Selected = 0;
    public void Select(int z)
    {
        Selected = z;
        Price.text = price[z].ToString();

        Name.transform.parent.gameObject.SetActive(true);
        ActPass.gameObject.SetActive(true);

        if (z < 3)
        {
            ActPass.text = "пассивный предмет";
            Name.text = Items[z].itemName;
            Name.transform.parent.GetComponent<Image>().color = ih.rarityColors[(int)Items[z].rarity];
            string[] txt = Items[z].descriptions[(int)Items[z].rarity].Split('&');
            Good.text = txt[0];
            if (txt.Length > 1)
                Bad.text = txt[1];
            else
            {
                Bad.text = "нет отрицательных свойств";
            }

            Good.transform.parent.gameObject.SetActive(true);
            Bad.transform.parent.gameObject.SetActive(true);
            Desc.transform.parent.gameObject.SetActive(false);


        }
        else if (z == 3)
        {
            ActPass.text = "активный предмет";
            Name.text = ActiveItems[0].itemName;
            Name.transform.parent.GetComponent<Image>().color = Color.red;
            Desc.text = ActiveItems[0].description;

            Good.transform.parent.gameObject.SetActive(false);
            Bad.transform.parent.gameObject.SetActive(false);
            Desc.transform.parent.gameObject.SetActive(true);
        }
        else if (z == 4) 
        {
            ActPass.text = "Улучшение";
            Name.text = "Снеки";
            Name.transform.parent.GetComponent<Image>().color = Color.black;
            Desc.text = "Восстанавливает 13 ХП";

            Good.transform.parent.gameObject.SetActive(false);
            Bad.transform.parent.gameObject.SetActive(false);
            Desc.transform.parent.gameObject.SetActive(true);
        }
        else if (z == 5)
        {
            ActPass.text = "Улучшение";
            Name.text = "Стимулятор ХП";
            Name.transform.parent.GetComponent<Image>().color = Color.black;
            Desc.text = "Увеличение максимального количества ХП на 25%";

            Good.transform.parent.gameObject.SetActive(false);
            Bad.transform.parent.gameObject.SetActive(false);
            Desc.transform.parent.gameObject.SetActive(true);

        }

    }
    public void Buy()
    {
        if (ch.CanBuyItems)
        {
            if (price[Selected] <= ch.money)
            {
                if (Selected < 3)
                {
                    if (ch.TryAddItem())
                    {
                        ch.AddItem(Items[Selected], Items[Selected].rarity);
                        Items[Selected] = null;
                        mic[Selected].Buyed = true;
                        mic[Selected].GetComponent<Image>().sprite = checkmark;
                        Kupuropriemnik.SetBool("Transaction", true);
                        StartCoroutine(Anim());
                    }
                    else
                        Debug.Log("Нет места");
                }
                else if (Selected == 3)
                {
                    if (ch.TryAddActiveItem())
                    {
                        ch.AddActiveItem(ActiveItems[0]);
                        ActiveItems[0] = null;
                        mic[Selected].Buyed = true;
                        mic[Selected].GetComponent<Image>().sprite = checkmark;
                        Kupuropriemnik.SetBool("Transaction", true);
                        StartCoroutine(Anim());
                    }
                    else
                        Debug.Log("Нет места");
                }
                else if (Selected == 4)
                {
                    ch.Heal(13);
                }
                else if (Selected == 5)
                {
                    var hp = ch.MaxHealth;
                    ch.MaxHealth = (int)(ch.MaxHealth * 1.25f);
                    ch.Heal(ch.MaxHealth - hp);
                }
                ch.money -= price[Selected];

                ch.AddStats(Stats.Statistics.MoneySpend, price[Selected]);

            }
            else
            {
                Debug.Log("Недостаточно средств");
            }
        }
    }
    IEnumerator Anim()
    {
        yield return new WaitForSeconds(1);
        Kupuropriemnik.SetBool("Transaction", false);
    }
    public void Exit()
    {
        transform.parent.parent.GetComponent<Window>().CloseWindow();
        ch.ClearRoom();
        ch.CanMove = true;
    }
    void Start()
    {
        ch = Camera.main.GetComponent<Character>();
        ih = Camera.main.GetComponent<ItemsHolder>();

        for (int i = 0; i < mic.Length; i++)
        {
            mic[i].shop = this;
            mic[i].GetComponent<MicroIcons>().id = i;
        }

        Reroll();
    }
    [SerializeField] TMP_Text balik;
    void Update()
    {
        balik.text = ch.money.ToString();
        if(ch.money < price[Selected])
        {
            BuyButton.transition = Selectable.Transition.ColorTint;
            var tmp = new ColorBlock();
            tmp.normalColor = BuyButton.colors.normalColor;
            tmp.highlightedColor = Color.red;
            tmp.selectedColor = BuyButton.colors.selectedColor;
            tmp.disabledColor = BuyButton.colors.disabledColor;
            tmp.pressedColor = BuyButton.colors.pressedColor;
            tmp.colorMultiplier = 1;
            BuyButton.colors = tmp;
        }
        else
        {
            BuyButton.transition = Selectable.Transition.ColorTint;
            var tmp = new ColorBlock();
            tmp.normalColor = BuyButton.colors.normalColor;
            tmp.highlightedColor = Color.yellow;
            tmp.selectedColor = BuyButton.colors.selectedColor;
            tmp.disabledColor = BuyButton.colors.disabledColor;
            tmp.pressedColor = BuyButton.colors.pressedColor;
            tmp.colorMultiplier = 1;
            BuyButton.colors = tmp;
        }
    }
}
