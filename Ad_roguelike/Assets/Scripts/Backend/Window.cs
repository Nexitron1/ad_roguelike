using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Window : MonoBehaviour {

    //переменные
    public WindowProperties windowProperties;
    public bool isHolded;
    public Vector3 mouse, offset;
    public Icon MyIcon;
    public MiniIcon MyMiniIcon;
    public GameObject Krest;

    private void Start()
    {
        if(!windowProperties.HasExitKrest)
            Krest.SetActive(false);
    }
    private void Update()
    {
        //Настройка позиций и размеров гейм обжектов(сцен)
        transform.GetChild(0).localScale = new Vector2(windowProperties.DragTabSize.x, windowProperties.DragTabSize.y);
        transform.GetChild(0).position = new Vector2(transform.position.x, transform.position.y + windowProperties.BgSize.y / 2 + windowProperties.DragTabSize.y / 2);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = windowProperties.DragTabColor;
        transform.GetChild(1).localScale = new Vector2(windowProperties.BgSize.x, windowProperties.BgSize.y);
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = windowProperties.BgColor;
        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = windowProperties.Title;
        transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(windowProperties.DragTabSize.x, windowProperties.DragTabSize.y);
        transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(windowProperties.DragTabSize.x, windowProperties.DragTabSize.y);
        transform.GetChild(2).GetComponent<RectTransform>().position = new Vector2(transform.position.x, transform.position.y + windowProperties.BgSize.y / 2 + windowProperties.DragTabSize.y / 2);
        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = InvertColor(windowProperties.DragTabColor);
        transform.GetChild(2).GetChild(1).GetComponent<RectTransform>().position = new Vector3(transform.GetChild(0).position.x + windowProperties.DragTabSize.x / 2 - 0.125f, transform.GetChild(0).position.y);
        //

        //Механика перемещения с отступом клика
        if (isHolded) 
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse = DeleteZ(mouse);
            transform.position = mouse - offset;   
        }
        //
    }
    public void SetOffset(Vector3 x)
    {
        offset = DeleteZ(x);
    }
    public void CloseWindow() 
    { 
        WindowManager.windows.Remove(this);
        WindowManager.miniIcons.Remove(MyMiniIcon);
        MyIcon.ActiveWindow = false;
        WindowManager.ReorganizeMiniIcons();
        Destroy(MyMiniIcon.gameObject);
        Destroy(gameObject);
        
    }

    Color InvertColor(Color c)
    {
        if((c.r + c.g + c.b)/3 >= 0.5f)
        {
            return Color.black;
        }
        else
        {
            return Color.white;
        }
    }
    Vector3 DeleteZ(Vector3 z) //метод
    {
        return new Vector3(z.x, z.y, 0);
    }
}
