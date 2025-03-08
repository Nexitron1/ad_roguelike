using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WindowProperties : ScriptableObject
{
    public Vector2 BgSize = new Vector2(1, 1);
    public Color BgColor = Color.gray;
    public Vector2 DragTabSize = new Vector2(1, 0.25f);
    public Color DragTabColor = Color.white;
    public string Title = "Window";
    public GameObject Template;
    public bool HasExitKrest = true;

}
