using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    Character character;
    [SerializeField] Transform[] slots = new Transform[2];
    [SerializeField] TMP_Text[] Names, Descs;
    List<ActiveItem> items = new List<ActiveItem>();

    private void Start()
    {
        character = Camera.main.GetComponent<Character>();
        character.HotbarSet(this);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetChild(0).GetComponent<ActIcons>().id = i;
        }
    }


    public void AddItem(ActiveItem item)
    {
        if(items.Count < 2)
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
        items[id].OnActivate();
        character.OnActiveArtUsed();
    }

}
