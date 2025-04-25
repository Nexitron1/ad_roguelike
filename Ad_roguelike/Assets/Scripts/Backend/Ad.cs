using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ad : MonoBehaviour
{
    public TMP_Text TimerSecondsText, M1Text, M2Text, MaxHp, DurationTime, RealtimeText;
    public Slider hp, Duration;
    public Sprite[] adSprites;
    public Image ad;
    Character character;

    public Transform content;
    public GameObject AdFeaturePrefab;

    [SerializeField] TMP_Text MinHpText, MaxHpText;
    List<Slider> OverHps = new List<Slider>();
    [SerializeField] GameObject OverSliderPrefab;
    public Transform holder;
    [SerializeField] Color[] colors;
    float realtime;
    void Start()
    {
        character = Camera.main.GetComponent<Character>();
        character.ad = this;
        ad.sprite = adSprites[Random.Range(0, adSprites.Length)];

        SpawnSliders();

    }

    void SpawnSliders()
    {
        for (int i = OverHps.Count; i < SlidersCount(); i++)
        {
            var g = (Instantiate(OverSliderPrefab, Vector3.zero, Quaternion.identity).GetComponent<Slider>());
            g.transform.SetParent(holder, false);
            g.minValue = (i + 1) * character.MaxHealth;
            g.maxValue = (i + 2) * character.MaxHealth;
            g.fillRect.gameObject.GetComponent<Image>().color = GetColor(i);
            g.value = g.maxValue;
            OverHps.Add(g);
        }

        while (OverHps.Count > SlidersCount())
        {
            Destroy(OverHps[OverHps.Count - 1].gameObject);
            OverHps.RemoveAt(OverHps.Count - 1);
        }
    }

    Color GetColor(int index)
    {
        if (index < colors.Length)
            return colors[index];
        if(colors.Length > 0)
            return colors[colors.Length - 1];
        return Color.white;
    }
    public void RefreshFeatures()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        for (int i = 0; i < character.AdFeatures.Count; i++)
        {
            var g = Instantiate(AdFeaturePrefab, content);
            g.GetComponent<AdFeaturePicture>().Set(character.AdFeatures[i].myType, character.AdFeatures[i].myDiff);
        }
    }
    void Update()
    {
        RealtimeText.text = realtime.ToString("0.0");
        realtime += Time.deltaTime;
        MinHpText.text = (character.Health + character.OverHealth).ToString("0.0");
        MaxHpText.text = (character.MaxHealth + character.MaxHealth * SlidersCount()).ToString("0.0");
        SpawnSliders();
        for (int i = 0; i < OverHps.Count; i++)
        {
            var s = OverHps[i];
            s.minValue = (i + 1) * character.MaxHealth;
            s.maxValue = (i + 2) * character.MaxHealth;
            s.fillRect.gameObject.GetComponent<Image>().color = GetColor(i);
            s.value = s.maxValue;
        }
        if (OverHps.Count > 0)
            OverHps[OverHps.Count - 1].value = (character.OverHealth + character.Health);
    }

    int SlidersCount()
    {
        int Max = (int)character.MaxHealth;
        int Over = (int)character.OverHealth;
        int ret;
        if (Over == 0) return 0;
        if (Max == Over) return 1;
        ret = (Over / Max) + 1;
        return ret;
    }


}
