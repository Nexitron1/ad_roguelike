using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ad : MonoBehaviour
{
    public TMP_Text TimerSecondsText, M1Text, M2Text, MaxHp, DurationTime;
    public Slider hp, Duration;
    Character character;
    void Start()
    {
        character = Camera.main.GetComponent<Character>();
        character.StartTimer(this, 20f);
    }
    void Update()
    {

    }


}
