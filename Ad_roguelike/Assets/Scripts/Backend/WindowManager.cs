using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static List<Window> windows = new List<Window>();
    public static List<MiniIcon> miniIcons = new List<MiniIcon>();
    void Start()
    {
        windows.Clear();
        miniIcons.Clear();
        
    }
    void Update()
    {
        
    }


    public static void Up(Window w) //Метод поднимает нужный элемент из списка на самый верх
    {
        if (!windows.Contains(w)) 
        { 
            windows.Add(w);
        }
        else
        {
            windows.Remove(w);
            windows.Add(w);
        }

        for (int j = 0; j < windows.Count; j++)
        {
            windows[j].gameObject.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sortingOrder = 1 + 100 * j; //панелька за которую тянуть
            windows[j].gameObject.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sortingOrder = 0 + 100 * j; //заполнитель
            windows[j].gameObject.transform.GetChild(2).transform.GetComponent<Canvas>().sortingOrder = 2 + 100 * j; //канвас тайтла
            windows[j].gameObject.transform.GetChild(3).GetChild(0).GetChild(0).transform.GetComponent<Canvas>().sortingOrder = 3 + 100 * j; //канвас в темплейте
            windows[j].gameObject.transform.GetChild(3).GetChild(0).transform.GetComponent<SortOrder>().MoveChilds(j); 
        }
    }

    public static void Up(MiniIcon i) //Метод поднимает нужный элемент из списка на самый верх
    {
        if (!miniIcons.Contains(i))
        {
            miniIcons.Add(i);
        }
        else
        {
            miniIcons.Remove(i);
            miniIcons.Add(i);
        }    

    }
    public static void ReorganizeMiniIcons()
    {
        for (int j = 0; j < miniIcons.Count; j++)
        {
            miniIcons[j].transform.position = new Vector2(-7.65f + j * 0.75f, -4.65f);
        }
    }

    public static void CloseWindow(Window w)
    {
        if (w != null)
            w.gameObject.SetActive(false);
        else
        {
            Debug.LogError("w = null");
        }
    }
    public static void OpenWindow(Window w)
    {
        if (w != null)
        {
            w.gameObject.SetActive(true);
            Up(w);
        }
        else
        {
            Debug.LogError("w = null");
        }
    }

}
