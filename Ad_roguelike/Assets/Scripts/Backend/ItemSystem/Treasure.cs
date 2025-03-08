using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Treasure : MonoBehaviour
{
    public Transform ItemTr;
    public TMP_Text ActPass, Name, Desc, Good, Bad;
    public bool Act;
    public Color[] colors;
    ItemsHolder ih;
    Character ch;

    Item pass;
    ActiveItem act;
    void Start()
    {
        ih = Camera.main.GetComponent<ItemsHolder>();
        ch = Camera.main.GetComponent<Character>();
        ch.CanMove = false;
        StartCoroutine(ItemMove());


    }
    
    public void Take()
    {
        if (Act)
        {
            ch.AddActiveItem(act);
        }
        else
        {
            ch.AddItem(pass, pass.rarity);
        }

        transform.parent.parent.GetComponent<Window>().CloseWindow();
        ch.CanMove = true;
        ch.ClearRoom();
    }
    public void DontTake()
    {
        transform.parent.parent.GetComponent<Window>().CloseWindow();
        ch.CanMove = true;
        ch.ClearRoom();
    }

    IEnumerator ItemMove()
    {
        StartCoroutine(MoveTo(new Vector2(0, 0.5f)));



        if (Random.Range(0, 100) >= 70)
            Act = true;
        else
            Act = false;


        ActPass.gameObject.SetActive(true);
        if (Act)
        {

            act = ih.CreateActiveItem();

            ItemTr.GetComponent<SpriteRenderer>().sprite = act.icon;

            yield return new WaitForSeconds(0.5f);
            ActPass.text = "Активный предмет";
            Name.transform.parent.gameObject.SetActive(true);
            Desc.transform.parent.gameObject.SetActive(true);

            Name.text = act.itemName;
            Name.transform.parent.gameObject.GetComponent<Image>().color = Color.red;
            Desc.text = act.description;


        }
        else
        {

            pass = ih.CreateItem();

            ItemTr.GetComponent<SpriteRenderer>().sprite = pass.icon;

            yield return new WaitForSeconds(1f);
            ActPass.text = "Пассивный предмет";
            Name.transform.parent.gameObject.SetActive(true);
            Good.transform.parent.gameObject.SetActive(true);
            Bad.transform.parent.gameObject.SetActive(true);
            
            string[] txt = pass.descriptions[(int)pass.rarity].Split('&');

            Good.text = txt[0];
            if (txt.Length > 1)
                Bad.text = txt[1];

            Name.text = pass.itemName;
            Name.transform.parent.gameObject.GetComponent<Image>().color = ih.rarityColors[(int)pass.rarity];
        }
    }

    IEnumerator MoveTo(Vector2 endPos)
    {
        yield return new WaitForSeconds(0.5f);
        float MovementTime = 0.7f;
        Vector2 startPos = ItemTr.transform.localPosition;

        for (float i = 0; i < 1; i += MovementTime / (40 * MovementTime))
        {
            ItemTr.transform.localPosition = Vector2.Lerp(startPos, endPos, i);
            yield return new WaitForSeconds(MovementTime / 40);
        }
        ItemTr.transform.localPosition = endPos;
    }
}
