using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBarShop : MonoBehaviour
{
    [SerializeField] Slider HpSlider;
    [SerializeField] TMP_Text MinHpText, MaxHpText;    
    void Start()
    {
        
    }
    void Update()
    {
        //MinHpText.text = HpSlider.value.ToString();
    }
}
