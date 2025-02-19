using System.Collections;
using System.Collections.Generic;
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

    public static void Up(Window w) //����� ��������� ������ ������� �� ������ �� ����� ����
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
            windows[j].gameObject.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sortingOrder = 1 + 100 * j; //�������� �� ������� ������
            windows[j].gameObject.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sortingOrder = 0 + 100 * j; //�����������
            windows[j].gameObject.transform.GetChild(2).transform.GetComponent<Canvas>().sortingOrder = 2 + 100 * j; //������ ������
            windows[j].gameObject.transform.GetChild(3).GetChild(0).GetChild(0).transform.GetComponent<Canvas>().sortingOrder = 3 + 100 * j; //������ � ���������
        }
    }

    public static void Up(MiniIcon i) //����� ��������� ������ ������� �� ������ �� ����� ����
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
            w.gameObject.SetActive(true);
        else
        {
            Debug.LogError("w = null");
        }
    }

}
