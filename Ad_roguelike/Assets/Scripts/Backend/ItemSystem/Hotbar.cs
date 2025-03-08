using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Hotbar : MonoBehaviour
{
    Character character;
    [SerializeField] Transform[] slots = new Transform[2];
    [SerializeField] TMP_Text[] Names, Descs;
    [SerializeField] TMP_Text[] Times;
    public List<ActiveItem> items = new List<ActiveItem>();
    public Sprite EmptyIcon;
    private void Start()
    {
        character = Camera.main.GetComponent<Character>();
        character.HotbarSet(this);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetChild(0).GetComponent<ActIcons>().id = i;
        }
    }
    private void Update()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].myType != ActiveItem.Type.Infinite)
            {
                Times[i].text = items[i].UsesLeft.ToString() + " / " + items[i].MaxUses.ToString();
                Times[i].transform.parent.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, (float)items[i].UsesLeft / (float)items[i].MaxUses);
            }
            else
            {
                Times[i].text = items[i].TotalTimesUsed.ToString() + " / ∞";
                Times[i].transform.parent.GetComponent<Image>().color = Color.green;
            }
        }
    }
    public void Refresh()
    {
        slots[0].GetChild(0).GetComponent<Image>().sprite = EmptyIcon;
        Names[0].text = "";
        Descs[0].text = "";

        slots[1].GetChild(0).GetComponent<Image>().sprite = EmptyIcon;
        Names[1].text = "";
        Descs[1].text = "";

        Times[0].text = "";
        Times[0].transform.parent.GetComponent<Image>().color = Color.red;

        Times[1].text = "";
        Times[1].transform.parent.GetComponent<Image>().color = Color.red;
    }
    public void AddItem(ActiveItem item)
    {
        if(character.TryAddItem())
        {
            items.Add(item);
            slots[items.Count - 1].GetChild(0).GetComponent<Image>().sprite = item.largeIcon;
            Names[items.Count - 1].text = item.itemName;
            Descs[items.Count - 1].text = item.description;
        }
        else
        {
            Debug.LogError("Reached limit of items");
        }
    }

    public void ActivateItem(int id)
    {
        if (id < items.Count)
        {
            items[id].OnActivate();
            character.OnActiveArtUsed(id);
        }
    }

}
