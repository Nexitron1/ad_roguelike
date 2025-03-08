using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text[] sts;
    public enum Statistics
    {
        ArtsUsed,
        AdWatched,
        FishUsed,
        ItemsDeleted,
        ItemsBuyed,
        Waiting,
        HpHealed,
        MoneyRecived,
        MoneySpend,
        Luck,
        Unluck
    }
    private void Start()
    {
        Camera.main.GetComponent<Character>().SetStats(this);
    }
    public void Modify(int index, float value)
    {
        sts[index].text = value.ToString();
    }

}
