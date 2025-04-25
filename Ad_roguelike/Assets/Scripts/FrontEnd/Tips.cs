using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public GameObject Tip;
    public Image img;
    public TMP_Text TipText;
    private void Start()
    {
    }
    public void SetMessage(string t, Color col)
    {
        TipText.text = t;
        img.color = col;
    }
    private void OnMouseEnter()
    {
        Tip.SetActive(true);
    }

    private void OnMouseExit()
    {
        Tip.SetActive(false);
    }
}
