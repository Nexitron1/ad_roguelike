using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class AdFeature : ScriptableObject
{
    public Character ch;
    public string AdName;
    public enum AdType
    {
        None,
        Flat,
        Bezlika,
        Trevogn,
        Haotic,
        Poor,
        Old,
        Skup,
        Unlucky,
        Pathetic
    }
    public AdType myType;
    public enum Difficulty
    {
        easy,
        normal,
        hard,
        extreme
    }
    public Difficulty myDiff;
    public string[] diffDescs;
    public static AdFeature CreateFeature(AdType type)
    {
        return SwitchType(type);
    }
    public static AdFeature SwitchType(AdType type)
    {
        switch (type)
        {
            case AdType.None:
                return null;
            case AdType.Flat:
                return AdFeature.CreateInstance<Flat>();
            case AdType.Bezlika:
                return AdFeature.CreateInstance<Bezlika>();
            case AdType.Trevogn:
                return AdFeature.CreateInstance<Trevogn>();
            case AdType.Haotic:
                return AdFeature.CreateInstance<Haotic>();
            case AdType.Poor:
                return AdFeature.CreateInstance<Poor>();
            case AdType.Old:
                return AdFeature.CreateInstance<Old>();
            case AdType.Skup:
                return AdFeature.CreateInstance<Skup>();
            case AdType.Unlucky:
                return AdFeature.CreateInstance<Unlucky>();
            case AdType.Pathetic:
                return AdFeature.CreateInstance<Pathetic>();

        }
        return null;
    }

    /*
    switch (myDiff) 
    {
        case Difficulty.easy:
            break;
        case Difficulty.normal:
            break;
        case Difficulty.hard:
            break;
        case Difficulty.extreme:
            break;
    }
    */
    public void InitType(Difficulty d)
    {

    }
    public virtual void Init() { ch = Camera.main.GetComponent<Character>(); }
    public virtual void OnEachSec() { }
    public virtual void OnActiveArtUsed(int index) { }
    public virtual void OnFigthEnd() { }
    public virtual bool Random(float LuckChance)
    {
        float r = UnityEngine.Random.Range(0f, 1f);

        if (r < LuckChance)
        {
            ch.AddStats(Stats.Statistics.Luck, 1);
            return true;
        }
        ch.AddStats(Stats.Statistics.Unluck, 1);
        return false;
    }


}
